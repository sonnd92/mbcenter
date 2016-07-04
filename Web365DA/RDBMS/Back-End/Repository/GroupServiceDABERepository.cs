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
    public class GroupServiceDABERepository : BaseBE<tblGroupOfService>, IGroupServiceDABERepository
    {
        /// <summary>
        /// function get all data tblArticle
        /// </summary>
        /// <returns></returns>
        public List<GroupOfServiceItem> GetList(out int total, string name, int currentRecord, int numberRecord, string propertyNameSort, bool descending, bool isDelete = false)
        {
            var query = from p in web365db.tblGroupOfService
                        where p.Name.ToLower().Contains(name) && p.IsDeleted == isDelete
                        select p;

            total = query.Count();

            query = descending ? query.OrderByDescending(propertyNameSort) : query.OrderBy(propertyNameSort);

            return query.Select(p => new GroupOfServiceItem()
            {
                ID = p.Id,
                Name = p.Name,
                NameAscii = p.NameAscii,
                Summary = p.Summary,
                IsShow = p.IsShow
                
            }).Skip(currentRecord).Take(numberRecord).ToList();
        }

        public void ResetListPicture(int id, string listPictureId)
        {
            throw new NotImplementedException();
        }

        public void Show(int id)
        {
            var service = web365db.tblGroupOfService.SingleOrDefault(p => p.Id == id);
            service.IsShow = true;
            web365db.Entry(service).State = EntityState.Modified;
            web365db.SaveChanges();
        }

        public void Hide(int id)
        {
            var service = web365db.tblGroupOfService.SingleOrDefault(p => p.Id == id);
            service.IsShow = false;
            web365db.Entry(service).State = EntityState.Modified;
            web365db.SaveChanges();
        }


        public T GetListForTree<T>(bool isShow = true, bool isDelete = false)
        {
            var query = from p in web365db.tblGroupOfService
                        where p.IsShow == isShow && p.IsDeleted == isDelete
                        orderby p.Id descending
                        select new GroupOfServiceItem()
                        {
                            ID = p.Id,
                            Name = p.Name,
                            NameAscii = p.NameAscii
                        };

            return (T)(object)query.ToList();
        }

        public T GetItemById<T>(int id)
        {
            var result = GetById<tblGroupOfService>(id);
            return (T) (object) new GroupOfServiceItem()
            {
                ID = result.Id,
                Summary = result.Summary,
                IsDeleted = result.IsDeleted,
                Name = result.Name,
                NameAscii = result.NameAscii,
                ServiceId = result.ServiceId
            };
        }

        public bool NameExist(int id, string name)
        {
            var query = web365db.tblGroupOfService.Count(c => c.Name.ToLower() == name.ToLower() && c.Id != id);

            return query > 0;
        }
    }
}
