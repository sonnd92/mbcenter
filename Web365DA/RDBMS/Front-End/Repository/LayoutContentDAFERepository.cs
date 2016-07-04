using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Web365Base;
using Web365DA.RDBMS.Front_End.IRepository;
using Web365Domain;
using Web365Utility;

namespace Web365DA.RDBMS.Front_End.Repository
{
    public class LayoutContentDAFERepository : BaseFE, ILayoutContentDAFERepository
    {
        public tblLayoutContent GetById(int id)
        {
            var query = from p in web365db.tblLayoutContent
                        where p.ID == id
                        orderby p.ID descending
                        select p;
            return query.FirstOrDefault();
        }

        public LayoutContentItem GetItemById(int id)
        {
            var product = new LayoutContentItem();

            var result = GetById(id);

            product = new LayoutContentItem()
            {
                ID = result.ID,
                Name = result.Name,
                NameAscii = result.NameAscii,
                SEOTitle = result.SEOTitle,
                SEODescription = result.SEODescription,
                SEOKeyword = result.SEOKeyword,
                DateCreated = result.DateCreated,
                DateUpdated = result.DateUpdated,
                PictureID = result.PictureID,
                Summary = result.Summary,
                Detail = result.Detail,
                IsShow = result.IsShow,
                UrlPicture = result.tblPicture != null ? result.tblPicture.FileName : string.Empty
            };

            return product;
        }
    }
}
