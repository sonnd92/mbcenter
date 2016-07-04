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
    public class ServiceDABERepository : BaseBE<tblService>, IServiceDABERepository
    {
        /// <summary>
        /// function get all data tblArticle
        /// </summary>
        /// <returns></returns>
        public List<ServiceItem> GetList(out int total, string name, int currentRecord, int numberRecord, string propertyNameSort, bool descending, bool isDelete = false)
        {
            var query = from p in web365db.tblService
                        where p.Title.ToLower().Contains(name) && p.IsDeleted == isDelete
                        select p;

            total = query.Count();

            query = descending ? query.OrderByDescending(propertyNameSort) : query.OrderBy(propertyNameSort);

            return query.Select(p => new ServiceItem()
            {
                ID = p.Id,
                Title = p.Title,
                TitleAscii = p.TitleAscii,
                Summary = p.Summary,
                IsShow = p.IsShow,
                Index = p.Index
                
            }).Skip(currentRecord).Take(numberRecord).ToList();
        }

        public void Show(int id)
        {
            var service = web365db.tblService.SingleOrDefault(p => p.Id == id);
            service.IsShow = true;
            web365db.Entry(service).State = EntityState.Modified;
            web365db.SaveChanges();
        }

        public void Hide(int id)
        {
            var service = web365db.tblService.SingleOrDefault(p => p.Id == id);
            service.IsShow = false;
            web365db.Entry(service).State = EntityState.Modified;
            web365db.SaveChanges();
        }


        public T GetListForTree<T>(bool isShow = true, bool isDelete = false)
        {
            var query = from p in web365db.tblService
                        where p.IsShow == isShow && p.IsDeleted == isDelete
                        orderby p.Id descending
                        select new ServiceItem()
                        {
                            ID = p.Id,
                            Name = p.Title,
                            NameAscii = p.TitleAscii
                        };

            return (T)(object)query.ToList();
        }

        public T GetItemById<T>(int id)
        {
            var result = GetById<tblService>(id);
            return (T) (object) new ServiceItem()
            {
                ID = result.Id,
                Summary = result.Summary,
                IsDeleted = result.IsDeleted,
                Title = result.Title,
                TitleAscii = result.TitleAscii,
                PictureId = result.PictureId,
                IsShow = result.IsShow,
                IsFeartured = result.IsFeartured,
                SEODescription = result.SEODescription,
                SEOKeyword = result.SEOKeyword,
                SEOTitle = result.SEOTitle
            };
        }

        public bool NameExist(int id, string name)
        {
            var query = web365db.tblService.Count(c => c.Title.ToLower() == name.ToLower() && c.Id != id);

            return query > 0;
        }
    }
}
