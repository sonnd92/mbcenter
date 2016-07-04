using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Web365Base;
using Web365DA.RDBMS.Front_End.IRepository;
using Web365Domain;
using Web365Domain.Service;
using Web365Utility;

namespace Web365DA.RDBMS.Front_End.Repository
{
    public class OtherDAFERepository : BaseFE, IOtherDAFERepository
    {
        public bool AddContact(tblContact contact)
        {
            try
            {
                web365db.tblContact.Add(contact);
                var result = web365db.SaveChanges();

                return result > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void AddEmailPromotion(tblReceiveInfo info)
        {
            web365db.tblReceiveInfo.Add(info);
            web365db.SaveChanges();
        }

        public List<ServiceItem> GetServiceItems()
        {
            var query = from c in web365db.tblService
                        where c.IsDeleted == false && c.IsShow == true
                        select new ServiceItem()
                        {
                            ID = c.Id,
                            Title = c.Title,
                            TitleAscii = c.TitleAscii,
                            Summary = c.Summary,
                            DateCreated = c.DateCreated,
                            Picture = new PictureItem()
                            {
                                ID = c.tblPicture != null ? c.tblPicture.ID : 0,
                                FileName = c.tblPicture != null ? c.tblPicture.FileName : string.Empty
                            },
                            IsFeartured = c.IsFeartured,
                            SEODescription = c.SEODescription,
                            SEOKeyword = c.SEOKeyword,
                            SEOTitle = c.SEOTitle
                        };
            return query.ToList();
        }

        public ServiceItem GetServiceItemByNameascii(string nameAscii)
        {
            var query = from c in web365db.tblService
                        where c.IsDeleted == false && c.IsShow == true && c.TitleAscii == nameAscii
                        select new ServiceItem()
                        {
                            ID = c.Id,
                            Title = c.Title,
                            TitleAscii = c.TitleAscii,
                            Summary = c.Summary,
                            DateCreated = c.DateCreated,
                            Picture = new PictureItem()
                            {
                                ID = c.tblPicture != null ? c.tblPicture.ID : 0,
                                FileName = c.tblPicture != null ? c.tblPicture.FileName : string.Empty
                            },
                            SEODescription = c.SEODescription,
                            SEOKeyword = c.SEOKeyword,
                            SEOTitle = c.SEOTitle
                        };
            return query.FirstOrDefault();

        }

        public List<StepOfServiceItem> GetStepOfServiceItems(int id)
        {
            var query = from c in web365db.tblStepOfService
                        where c.IsShow == true && c.IsDeleted == false && c.ServiceId == id
                        orderby c.Step
                        select new StepOfServiceItem()
                         {
                             ID = c.Id,
                             DateCreated = c.DateCreated,
                             Uses = c.Uses,
                             Course = c.Course,
                             Step = c.Step,
                             SlidesPictureItems = c.tblPicture.Select(p => new PictureItem()
                             {
                                 FileName = p.FileName,
                                 Summary = p.Summary
                             }).ToList()

                         };
            return query.ToList();

        }

        public List<GroupOfServiceItem> GetDetailServiceItems(int id)
        {
            var query = from g in web365db.tblGroupOfService
                        where g.IsShow == true && g.IsDeleted == false && g.ServiceId == id
                        select new GroupOfServiceItem()
                        {
                            ID = g.Id,
                            Name = g.Name,
                            NameAscii = g.NameAscii,
                            Summary = g.Summary,
                            DetailServiceItems =
                                g.tblServiceDetail.OrderBy(d => d.Index).Where(d => d.IsShow == true && d.IsDeleted == false)
                                    .Select(d => new DetailServiceItem()
                                    {
                                        ID = d.Id,
                                        Name = d.Name,
                                        Detail = d.Detail,
                                        Price = d.Price

                                    }).ToList()
                        };
            return query.ToList();
        }
    }
}
