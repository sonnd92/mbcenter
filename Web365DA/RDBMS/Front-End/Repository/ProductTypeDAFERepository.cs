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
    public class ProductTypeDAFERepository : BaseFE, IProductTypeDAFERepository
    {
        public tblTypeProduct GetById(int id)
        {
            var query = from p in web365db.tblTypeProduct
                        where p.ID == id
                        orderby p.ID descending
                        select p;
            return query.FirstOrDefault();
        }

        public ProductTypeItem GetByParentAsciiAndAscii(string parentAscii, string ascii)
        {
            var productType = new ProductTypeItem();

            var result = (from p in web365db.tblTypeProduct
                          where p.tblTypeProduct2.NameAscii == parentAscii && p.NameAscii == ascii && p.LanguageId == LanguageId && p.IsShow == true && p.IsDeleted == false
                          orderby p.ID descending
                          select p).FirstOrDefault();

            if (result != null)
            {
                productType = new ProductTypeItem()
                {
                    ID = result.ID,
                    Name = result.Name,
                    NameAscii = result.NameAscii,
                    ParentName = result.tblTypeProduct2.Name,
                    ParentNameAscii = result.tblTypeProduct2.NameAscii,
                    Detail = result.Detail,
                    IsShow = result.IsShow,
                    ListPicture = result.tblPicture1.Select(p => new PictureItem()
                    {
                        ID = p.ID,
                        Link = p.Link,
                        FileName = p.FileName

                    }).ToList()
                };
            }

            return productType;
        }

        public ProductTypeItem GetItemById(int id)
        {
            var product = new ProductTypeItem();

            var result = GetById(id);

            product = new ProductTypeItem()
            {
                ID = result.ID,
                Name = result.Name,
                NameAscii = result.NameAscii,
                SEOTitle = result.SEOTitle,
                SEODescription = result.SEODescription,
                SEOKeyword = result.SEOKeyword,
                DateCreated = result.DateCreated,
                DateUpdated = result.DateUpdated,
                Number = result.Number,
                PictureID = result.PictureID,
                Detail = result.Detail,
                IsShow = result.IsShow
            };

            return product;
        }

        public ProductTypeItem GetByAsciiAndParent(string ascii, int parentId)
        {
            var product = new ProductTypeItem();

            var query = from p in web365db.tblTypeProduct
                        where p.NameAscii == ascii && p.Parent == parentId && p.LanguageId == LanguageId
                        orderby p.ID descending
                        select new ProductTypeItem()
                        {
                            ID = p.ID,
                            Name = p.Name,
                            NameAscii = p.NameAscii,
                            ParentName = p.tblTypeProduct2.Name,
                            ParentNameAscii = p.tblTypeProduct2.NameAscii,
                            SEOTitle = p.SEOTitle,
                            SEODescription = p.SEODescription,
                            SEOKeyword = p.SEOKeyword,
                            DateCreated = p.DateCreated,
                            DateUpdated = p.DateUpdated,
                            Number = p.Number,
                            PictureID = p.PictureID,
                            Detail = p.Detail,
                            IsShow = p.IsShow
                        };

            product = query.FirstOrDefault();

            return product;
        }

        public List<ProductTypeItem> GetListByParent(int? parentId)
        {
            var list = new List<ProductTypeItem>();

            var query = from p in web365db.tblTypeProduct
                        where p.Parent == parentId && p.LanguageId == LanguageId && p.IsShow == true && p.IsDeleted == false
                        orderby p.Number descending, p.ID descending
                        select new ProductTypeItem()
                        {
                            ID = p.ID,
                            Name = p.Name,
                            NameAscii = p.NameAscii,
                            Number = p.Number,
                            ParentName = p.tblTypeProduct2.Name,
                            ParentNameAscii = p.tblTypeProduct2.NameAscii
                        };

            list = query.ToList();

            return list;
        }

        public List<ProductTypeItem> GetAllChildByParent(int? parentId)
        {
            var list = new List<ProductTypeItem>();

            var query = web365db.Database.SqlQuery<ProductTypeItem>("EXEC [dbo].[PRC_Product_GetAllChildTypeByParentID] {0}", parentId);

            list = query.Select(p => new ProductTypeItem()
            {
                ID = p.ID,
                Parent = p.Parent,
                Name = p.Name,
                NameAscii = p.NameAscii,
                Number = p.Number,
                IsShow = p.IsShow
            }).ToList();

            return list;
        }

        public List<ProductTypeItem> GetByGroup(int groupId)
        {
            var list = new List<ProductTypeItem>();

            var query = from p in web365db.tblTypeProduct
                        where p.tblProductType_Group_Map.Any(g => g.GroupTypeID == groupId) && p.LanguageId == LanguageId && p.IsShow == true && p.IsDeleted == false
                        orderby p.ID descending
                        select new ProductTypeItem()
                        {
                            ID = p.ID,
                            Name = p.Name,
                            NameAscii = p.NameAscii,
                            Number = p.Number,
                            ParentName = p.tblTypeProduct2.Name,
                            ParentNameAscii = p.tblTypeProduct2.NameAscii
                        };

            list = query.ToList();

            return list;
        }
    }
}
