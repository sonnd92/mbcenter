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
    public class StepServiceDABERepository : BaseBE<tblStepOfService>, IStepServiceDABERepository
    {
        /// <summary>
        /// function get all data tblArticle
        /// </summary>
        /// <returns></returns>
        public List<StepOfServiceItem> GetList(out int total, string name, int currentRecord, int numberRecord, string propertyNameSort, bool descending, bool isDelete = false)
        {
            var query = from p in web365db.tblStepOfService
                        where p.Name.ToLower().Contains(name) && p.IsDeleted == isDelete
                        select p;

            total = query.Count();

            query = descending ? QueryableHelper.OrderByDescending(query, propertyNameSort) : QueryableHelper.OrderBy(query, propertyNameSort);

            return query.Select(p => new StepOfServiceItem()
            {
                ID = p.Id,
                Name = p.Name,
                Course = p.Course,
                DateCreated = p.DateCreated,
                Step = p.Step,
                SlidesPictureItems = p.tblPicture.Select(c => new PictureItem()
                {
                    ID = c.ID,
                    FileName = c.FileName,
                    Summary = c.Summary
                }).ToList(),
                IsShow = p.IsShow ,
                ServiceId = p.ServiceId
            }).Skip(currentRecord).Take(numberRecord).ToList();
        }

        public T GetItemById<T>(int id)
        {
            var result = GetById<tblStepOfService>(id);

            return (T)(object)new StepOfServiceItem()
            {
                ID = result.Id,
                DateCreated = result.DateCreated,
                Step = result.Step,
                Uses = result.Uses,
                Name = result.Name,
                Course = result.Course,
                ServiceId = result.ServiceId,
                SlidesPictureItems = result.tblPicture.Select(c => new PictureItem()
                {
                    ID = c.ID,
                    FileName = c.FileName,
                    Summary = c.Summary
                }).ToList(),
                IsShow = result.IsShow,
                ListPictureID = result.tblPicture.Select(c=>c.ID).ToArray()
            };
        }

        public void Show(int id)
        {
            var article = web365db.tblStepOfService.SingleOrDefault(p => p.Id == id);
            article.IsShow = true;
            web365db.Entry(article).State = EntityState.Modified;
            web365db.SaveChanges();
        }

        public void Hide(int id)
        {
            var article = web365db.tblStepOfService.SingleOrDefault(p => p.Id == id);
            article.IsShow = false;
            web365db.Entry(article).State = EntityState.Modified;
            web365db.SaveChanges();
        }

        public void ResetListPicture(int id, string listPictureId)
        {
            web365db.Database.SqlQuery<object>("EXEC [dbo].[PRC_ResetPictureStepService] {0}, {1}", id, listPictureId).FirstOrDefault();
        }

        public T GetListForTree<T>(bool isShow = true, bool isDelete = false)
        {
            throw new NotImplementedException();
        }

        public bool NameExist(int id, string name)
        {
            throw new NotImplementedException();
        }
    }
}
