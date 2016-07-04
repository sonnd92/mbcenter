﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using Web365Base;
using Web365DA.RDBMS.Back_End.IRepository;
using Web365Domain;
using Web365Utility;

namespace Web365DA.RDBMS.Back_End.Repository
{
    /// <summary>
    /// create by BienLV 05-01-2013
    /// </summary>
    public class ContactDABERepository : BaseBE<tblContact>, IContactDABERepository
    {
        /// <summary>
        /// function get all data tblTypeProduct
        /// </summary>
        /// <returns></returns>
        public List<ContactItem> GetList(out int total, string name, string title, DateTime? fromDate, DateTime? toDate, int currentRecord, int numberRecord, string propertyNameSort, bool descending, bool isDelete = false)
        {
            var query = from p in web365db.tblContact
                        where p.Name.ToLower().Contains(name) && p.Title.ToLower().Contains(title) && p.IsDeleted == isDelete
                        select p;

            total = query.Count();

            if(fromDate.HasValue)
            {
                query = query.Where(c => c.DateCreated >= fromDate);
            }

            if (toDate.HasValue)
            {
                query = query.Where(c => c.DateCreated >= toDate);
            }

            query = descending ? QueryableHelper.OrderByDescending(query, propertyNameSort) : QueryableHelper.OrderBy(query, propertyNameSort);

            return query.Select(p => new ContactItem()
            {
                ID = p.ID,
                Title = p.Title,
                Name = p.Name,
                Email = p.Email,
                Phone = p.Phone,
                DateCreated = p.DateCreated,
                IsViewed = p.IsViewed
            }).Skip(currentRecord).Take(numberRecord).ToList();
        }

        public T GetListForTree<T>(bool isShow = true, bool isDelete = false)
        {
            var query = from p in web365db.tblContact
                        where p.IsViewed == isShow && p.IsDeleted == isDelete
                        orderby p.ID descending
                        select new ContactItem()
                        {
                            ID = p.ID,
                            Name = p.Name
                        };

            return (T)(object)query.ToList();
        }        

        /// <summary>
        /// get product type item
        /// </summary>
        /// <param name="id">id of product type</param>
        /// <returns></returns>
        public T GetItemById<T>(int id)
        {
            var result = GetById<tblContact>(id);

            return (T)(object)new ContactItem()
                        {
                            ID = result.ID,
                            Name = result.Name,
                            Address = result.Address,
                            Phone = result.Phone,
                            Title = result.Title,
                            Message = result.Message,
                            Email= result.Email,
                            Gender = result.Gender,
                            UpdatedBy = result.UpdatedBy,
                            DateCreated = result.DateCreated,
                            DateUpdated = result.DateUpdated,
                            IsViewed = result.IsViewed
                        };
        }        

        #region Insert, Edit, Delete, Save        

        public void Show(int id)
        {
            var Contact = web365db.tblContact.SingleOrDefault(p => p.ID == id);
            Contact.IsViewed = true;
            web365db.Entry(Contact).State = EntityState.Modified;
            web365db.SaveChanges();
        }

        public void Hide(int id)
        {
            var Contact = web365db.tblContact.SingleOrDefault(p => p.ID == id);
            Contact.IsViewed = false;
            web365db.Entry(Contact).State = EntityState.Modified;
            web365db.SaveChanges();
        }

        #endregion

        #region Check
        public bool NameExist(int id, string name)
        {
            var query = web365db.tblContact.Count(c => c.Name.ToLower() == name.ToLower() && c.ID != id);

            return query > 0;
        }
        #endregion
    }
}
