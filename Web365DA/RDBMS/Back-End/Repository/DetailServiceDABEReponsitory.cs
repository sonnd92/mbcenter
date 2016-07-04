using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web365Base;
using Web365DA.RDBMS.Back_End.IRepository;
using Web365Domain;
using Web365Domain.Service;
using Web365Utility;

namespace Web365DA.RDBMS.Back_End.Repository
{
    public class DetailServiceDABEReponsitory :BaseBE<tblServiceDetail>, IDetailServiceDABEReponsitory
    {
        /// <summary>
        /// function get all data tblArticle
        /// </summary>
        /// <returns></returns>
        public List<DetailServiceItem> GetList(out int total, string name, int currentRecord, int numberRecord, string propertyNameSort, bool descending, bool isDelete = false)
        {
            var query = from p in web365db.tblServiceDetail
                where p.Name.ToLower().Contains(name) && p.IsDeleted == isDelete
                select p;

            total = query.Count();

            query = descending ? QueryableHelper.OrderByDescending(query, propertyNameSort) : QueryableHelper.OrderBy(query, propertyNameSort);

            return query.Select(p => new DetailServiceItem()
            {
                ID = p.Id,
                Name = p.Name,
                Detail = p.Detail,
                IsShow = p.IsShow,
                DateCreated = p.DateCreated,
                Price = p.Price,
                IsDeleted = p.IsDeleted,
                GroupId = p.GroupId ,
                Index = p.Index
            }).Skip(currentRecord).Take(numberRecord).ToList();
        }

        public T GetItemById<T>(int id)
        {
            var result = GetById<tblServiceDetail>(id);

            return (T)(object)new DetailServiceItem()
            {
                ID = result.Id,
                DateCreated = result.DateCreated,
                DateUpdated = result.DateUpdated,
                Detail = result.Detail,
                IsShow = result.IsShow,
                Name = result.Name,
                IsDeleted = result.IsDeleted,
                Price = result.Price,
                GroupId = result.GroupId,
                Index = result.Index
            };
        }

        public void Show(int id)
        {
            var article = web365db.tblServiceDetail.SingleOrDefault(p => p.Id == id);
            article.IsShow = true;
            web365db.Entry(article).State = EntityState.Modified;
            web365db.SaveChanges();
        }

        public void Hide(int id)
        {
            var article = web365db.tblServiceDetail.SingleOrDefault(p => p.Id == id);
            article.IsShow = false;
            web365db.Entry(article).State = EntityState.Modified;
            web365db.SaveChanges();
        }

        #region Check
        public bool NameExist(int id, string name)
        {
            var query = web365db.tblServiceDetail.Count(c => c.Name.ToLower() == name.ToLower() && c.Id != id);

            return query > 0;
        }
        #endregion

        public T GetListForTree<T>(bool isShow = true, bool isDelete = false)
        {
            throw new NotImplementedException();
        }
    }
}
