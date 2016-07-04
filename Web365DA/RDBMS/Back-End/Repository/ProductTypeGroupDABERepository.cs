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
    public class ProductTypeGroupDABERepository : BaseBE<tblProductTypeGroup>, IProductTypeGroupDABERepository
    {
        /// <summary>
        /// function get all data tblProductTypeGroup
        /// </summary>
        /// <returns></returns>
        public List<ProductTypeGroupItem> GetList(out int total, string name, int currentRecord, int numberRecord, string propertyNameSort, bool descending, bool isDelete = false)
        {
            var query = from p in web365db.tblProductTypeGroup
                        where p.Name.ToLower().Contains(name) && p.IsDeleted == isDelete
                        select p;

            total = query.Count();

            query = descending ? QueryableHelper.OrderByDescending(query, propertyNameSort) : QueryableHelper.OrderBy(query, propertyNameSort);

            return query.Select(p => new ProductTypeGroupItem()
            {
                ID = p.ID,
                Name = p.Name,
                NameAscii = p.NameAscii,
                SEOTitle = p.SEOTitle,
                SEODescription = p.SEODescription,
                SEOKeyword = p.SEOKeyword,
                Number = p.Number,
                Summary = p.Summary,
                Detail = p.Detail,
                IsShow = p.IsShow
            }).Skip(currentRecord).Take(numberRecord).ToList();
        }

        public T GetListForTree<T>(bool isShow = true, bool isDelete = false)
        {
            var query = from p in web365db.tblProductTypeGroup
                        where p.IsShow == isShow && p.IsDeleted == isDelete
                        orderby p.ID descending
                        select new ProductTypeGroupItem()
                        {
                            ID = p.ID,
                            Name = p.Name,
                            NameAscii = p.NameAscii
                        };

            return (T)(object)query.ToList();
        }
        
        public T GetItemById<T>(int id)
        {
            var query = from p in web365db.tblProductTypeGroup
                        where p.ID == id
                        orderby p.ID descending
                        select new ProductTypeGroupItem()
                        {
                            ID = p.ID,
                            Name = p.Name,
                            NameAscii = p.NameAscii,
                            SEOTitle = p.SEOTitle,
                            SEODescription = p.SEODescription,
                            SEOKeyword = p.SEOKeyword,
                            DateCreated = p.DateCreated,
                            DateUpdated = p.DateUpdated,
                            Number = p.Number,
                            PictureID = p.PictureID,
                            Summary = p.Summary,
                            Detail = p.Detail,
                            IsShow = p.IsShow
                        };
            return (T)(object)query.FirstOrDefault();
        }
        

        public void Show(int id)
        {
            var groupArticle = web365db.tblProductTypeGroup.SingleOrDefault(p => p.ID == id);
            groupArticle.IsShow = true;
            web365db.Entry(groupArticle).State = EntityState.Modified;
            web365db.SaveChanges();
        }

        public void Hide(int id)
        {
            var groupArticle = web365db.tblProductTypeGroup.SingleOrDefault(p => p.ID == id);
            groupArticle.IsShow = false;
            web365db.Entry(groupArticle).State = EntityState.Modified;
            web365db.SaveChanges();
        }

        #region Check
        public bool NameExist(int id, string name)
        {
            var query = web365db.tblProductTypeGroup.Count(c => c.Name.ToLower() == name.ToLower() && c.ID != id);

            return query > 0;
        }
        #endregion
    }
}
