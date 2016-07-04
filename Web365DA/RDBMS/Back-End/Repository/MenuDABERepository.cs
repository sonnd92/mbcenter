﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Web365Base;
using Web365DA.RDBMS.Back_End.IRepository;
using Web365Domain;
using Web365Utility;


namespace Web365DA.RDBMS.Back_End.Repository
{
    public class MenuDABERepository : BaseBE<tblMenu>, IMenuDABERepository
    {

        /// <summary>
        /// function get all data tblFile
        /// </summary>
        /// <returns></returns>
        public List<MenuItem> GetList(out int total, string name, int? parentId, int currentRecord, int numberRecord, string propertyNameSort, bool descending, bool isDelete = false)
        {
            var query = from p in web365db.tblMenu
                        where p.Name.ToLower().Contains(name) && p.IsDeleted == isDelete
                        select p;

            query = query.Where(p => p.LanguageId == LanguageId);

            if (parentId.HasValue)
            {
                query = query.Where(p => p.Parent == parentId);
            }

            total = query.Count();

            query = descending ? QueryableHelper.OrderByDescending(query, propertyNameSort) : QueryableHelper.OrderBy(query, propertyNameSort);

            return query.Select(p => new MenuItem()
            {
                ID = p.ID,
                Name = p.Name,
                NameAscii = p.NameAscii,
                DateCreated = p.DateCreated,
                IsShow = p.IsShow
            }).Skip(currentRecord).Take(numberRecord).ToList();
        }

        public T GetItemById<T>(int id)
        {
            var query = from p in web365db.tblMenu
                        where p.ID == id
                        orderby p.ID descending
                        select new MenuItem()
                        {
                            ID = p.ID,
                            Name = p.Name,
                            NameAscii = p.NameAscii,
                            Link = p.Link,
                            Target = p.Target,
                            CssClass = p.CssClass,
                            Parent = p.Parent,
                            DisplayOrder = p.DisplayOrder,
                            DateCreated = p.DateCreated,
                            DateUpdated = p.DateUpdated,
                            CreatedBy = p.CreatedBy,
                            UpdatedBy = p.UpdatedBy,
                            IsShow = p.IsShow
                        };
            return (T)(object)query.FirstOrDefault();
        }

        public void Show(int id)
        {
            var obj = web365db.tblMenu.SingleOrDefault(p => p.ID == id);
            obj.IsShow = true;
            web365db.Entry(obj).State = EntityState.Modified;
            web365db.SaveChanges();
        }

        public void Hide(int id)
        {
            var obj = web365db.tblMenu.SingleOrDefault(p => p.ID == id);
            obj.IsShow = false;
            web365db.Entry(obj).State = EntityState.Modified;
            web365db.SaveChanges();
        }

        #region Check
        public bool NameExist(int id, string name)
        {
            var query = web365db.tblMenu.Count(c => c.Name.ToLower() == name.ToLower() && c.LanguageId == LanguageId && c.ID != id);

            return query > 0;
        }
        #endregion

        public T GetListForTree<T>(bool isShow = true, bool isDelete = false)
        {
            var query = from p in web365db.tblMenu
                        where p.IsShow == isShow && p.IsDeleted == isDelete
                        select p;

            query = query.Where(p => p.LanguageId == LanguageId);

            return (T)(object)query.OrderByDescending(p => p.ID).Select(p => new MenuItem()
            {
                ID = p.ID,
                Name = p.Name,
                NameAscii = p.NameAscii,
                Parent = p.Parent
            }).ToList();
        }
    }
}
