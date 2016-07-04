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
    public class ProductGroupAttributeDABERepository : BaseBE<tblProductGroupAttribute>, IProductGroupAttributeDABERepository
    {
        /// <summary>
        /// function get all data tblProductGroupAttribute
        /// </summary>
        /// <returns></returns>
        public List<ProductGroupAttributeItem> GetList(out int total, string name, int currentRecord, int numberRecord, string propertyNameSort, bool descending, bool isDelete = false)
        {
            var query = from p in web365db.tblProductGroupAttribute
                        where p.Name.ToLower().Contains(name) && p.IsDeleted == isDelete
                        orderby p.ID descending
                        select new ProductGroupAttributeItem()
                        {
                            ID = p.ID,
                            Name = p.Name,
                            NameAscii = p.NameAscii,
                            Number = p.Number,
                            IsShow = p.IsShow
                        };

            total = query.Count();

            query = descending ? QueryableHelper.OrderByDescending(query, propertyNameSort) : QueryableHelper.OrderBy(query, propertyNameSort);

            return query.Skip(currentRecord).Take(numberRecord).ToList();
        }

        public T GetListForTree<T>(bool isShow = true, bool isDelete = false)
        {
            var query = from p in web365db.tblProductGroupAttribute
                        where p.IsShow == isShow && p.IsDeleted == isDelete
                        orderby p.ID descending
                        select new ProductGroupAttributeItem()
                        {
                            ID = p.ID,
                            Name = p.Name,
                            NameAscii = p.NameAscii
                        };

            return (T)(object)query.ToList();
        }

        public List<ProductGroupAttributeItem> GetListByProductType(int typeId, bool isShow = true, bool isDelete = false)
        {
            var query = from p in web365db.tblProductGroupAttribute
                        where p.ProductTypeID == typeId && p.IsShow == isShow && p.IsDeleted == isDelete
                        orderby p.Number descending
                        select new ProductGroupAttributeItem()
                        {
                            ID = p.ID,
                            Name = p.Name,
                            NameAscii = p.NameAscii
                        };

            return query.ToList();
        }

        /// <summary>
        /// get product type item
        /// </summary>
        /// <param name="id">id of product type</param>
        /// <returns></returns>
        public T GetItemById<T>(int id)
        {
            var query = from p in web365db.tblProductGroupAttribute
                        where p.ID == id
                        orderby p.ID descending
                        select new ProductGroupAttributeItem()
                        {
                            ID = p.ID,
                            Name = p.Name,
                            NameAscii = p.NameAscii,
                            Detail = p.Detail,
                            DateCreated = p.DateCreated,
                            DateUpdated = p.DateUpdated,
                            Number = p.Number,
                            ProductTypeID = p.ProductTypeID,
                            IsShow = p.IsShow
                        };
            return (T)(object)query.FirstOrDefault();
        }

        #region Insert, Edit, Delete, Save

        public void Show(int id)
        {
            var filterProduct = web365db.tblProductGroupAttribute.SingleOrDefault(p => p.ID == id);
            filterProduct.IsShow = true;
            web365db.Entry(filterProduct).State = EntityState.Modified;
            web365db.SaveChanges();
        }

        public void Hide(int id)
        {
            var filterProduct = web365db.tblProductGroupAttribute.SingleOrDefault(p => p.ID == id);
            filterProduct.IsShow = false;
            web365db.Entry(filterProduct).State = EntityState.Modified;
            web365db.SaveChanges();
        }

        #endregion

        #region Check
        public bool NameExist(int id, string name)
        {
            var query = web365db.tblProductGroupAttribute.Count(c => c.Name.ToLower() == name.ToLower() && c.ID != id);

            return query > 0;
        }
        #endregion
    }
}
