using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Web365Base;
using Web365DA.RDBMS.Front_End.IRepository;
using Web365Domain;
using Web365Models;
using Web365Utility;

namespace Web365DA.RDBMS.Front_End.Repository
{
    public class ArticleTypeDAFERepository : BaseFE, IArticleTypeDAFERepository
    {
        public ArticleTypeItem GetItemById(int id)
        {
            var articleType = new ArticleTypeItem();

            var result = (from p in web365db.tblTypeArticle
                          where p.ID == id && p.IsShow == true && p.IsDeleted == false
                          select p).FirstOrDefault();

            articleType = new ArticleTypeItem()
            {
                ID = result.ID,
                Parent = result.Parent,
                Name = result.Name,
                NameAscii = result.NameAscii,
                SEOTitle = result.SEOTitle,
                SEODescription = result.SEODescription,
                SEOKeyword = result.SEOKeyword,
                DateCreated = result.DateCreated,
                DateUpdated = result.DateUpdated,
                Number = result.Number,
                PictureID = result.PictureID,
                Summary = result.Summary,
                Detail = result.Detail,
                IsShow = result.IsShow,
                ListChildID = result.tblTypeArticle1.Where(t => t.IsShow == true && t.IsDeleted == false).OrderByDescending(t => t.Number).ThenByDescending(t => t.ID).Select(t => t.ID).ToArray()
            };

            return articleType;
        }

        public ArticleTypeItem GetItemByNameAscii(string nameAscii, bool isShow = true, bool isDeleted = false)
        {
            var articleType = new ArticleTypeItem();

            articleType = (from p in web365db.tblTypeArticle
                           where p.NameAscii == nameAscii && p.LanguageId == LanguageId && p.IsShow == isShow && p.IsDeleted == isDeleted
                           orderby p.ID descending
                           select new ArticleTypeItem()
                           {
                               ID = p.ID,
                               Parent = p.Parent,
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
                           }).FirstOrDefault();

            return articleType;
        }

        public List<ArticleTypeItem> GetListByGroup(int groupId, bool isShow = true, bool isDeleted = false)
        {
            var list = new List<ArticleTypeItem>();

            list = (from p in web365db.tblGroup_TypeArticle_Map
                    where p.GroupTypeID == groupId && p.tblTypeArticle.LanguageId == LanguageId && p.tblTypeArticle.IsShow == isShow && p.tblTypeArticle.IsDeleted == isDeleted
                    orderby p.DisplayOrder descending
                    select new ArticleTypeItem()
                    {
                        ID = p.tblTypeArticle.ID,
                        Parent = p.tblTypeArticle.Parent,
                        Name = p.tblTypeArticle.Name,
                        NameAscii = p.tblTypeArticle.NameAscii,
                        SEOTitle = p.tblTypeArticle.SEOTitle,
                        SEODescription = p.tblTypeArticle.SEODescription,
                        SEOKeyword = p.tblTypeArticle.SEOKeyword,
                        Number = p.tblTypeArticle.Number,
                        IsShow = p.tblTypeArticle.IsShow
                    }).ToList();

            return list;
        }

        public List<ArticleTypeItem> GetListByParent(int[] parent, bool isShow = true, bool isDeleted = false)
        {
            var list = new List<ArticleTypeItem>();

            list = (from p in web365db.tblTypeArticle
                    where parent.Contains(p.Parent.Value) && p.tblArticle.Any() && p.IsShow == isShow && p.IsDeleted == isDeleted
                    orderby p.Number descending, p.ID descending
                    select new ArticleTypeItem()
                    {
                        ID = p.ID,
                        Name = p.Name,
                        NameAscii = p.NameAscii,
                        SEOTitle = p.SEOTitle,
                        SEODescription = p.SEODescription,
                        SEOKeyword = p.SEOKeyword,
                        Number = p.Number,
                        IsShow = p.IsShow,
                        Parent = p.Parent,
                        ParentName = p.tblTypeArticle2.Name,
                        ParentAscii = p.tblTypeArticle2.NameAscii
                    }).ToList();

            return list;
        }

        public List<ArticleTypeItem> GetListByParent(int[] parent, int skip, int top, bool isShow = true, bool isDeleted = false)
        {
            var list = new List<ArticleTypeItem>();

            list = (from p in web365db.tblTypeArticle
                    where parent.Contains(p.Parent.Value) && p.IsShow == isShow && p.IsDeleted == isDeleted
                    orderby p.ID descending
                    select new ArticleTypeItem()
                    {
                        ID = p.ID,
                        Name = p.Name,
                        NameAscii = p.NameAscii,
                        SEOTitle = p.SEOTitle,
                        SEODescription = p.SEODescription,
                        SEOKeyword = p.SEOKeyword,
                        Number = p.Number,
                        IsShow = p.IsShow,
                        Parent = p.Parent,
                        ParentName = p.tblTypeArticle2.Name,
                        ParentAscii = p.tblTypeArticle2.NameAscii
                    }).Skip(skip).Take(top).ToList();

            return list;
        }

        public List<ArticleTypeItem> GetListByParentAscii(string parentAscii, bool isShow = true, bool isDeleted = false)
        {
            var list = new List<ArticleTypeItem>();

            list = (from p in web365db.tblTypeArticle
                    where p.tblTypeArticle2.NameAscii == parentAscii && p.IsShow == isShow && p.IsDeleted == isDeleted
                    orderby p.Number descending, p.ID descending
                    select new ArticleTypeItem()
                    {
                        ID = p.ID,
                        Name = p.Name,
                        NameAscii = p.NameAscii,
                        SEOTitle = p.SEOTitle,
                        SEODescription = p.SEODescription,
                        SEOKeyword = p.SEOKeyword,
                        Number = p.Number,
                        IsShow = p.IsShow,
                        Parent = p.Parent,
                        UrlPicture = p.tblPicture.FileName,
                        ParentName = p.tblTypeArticle2.Name,
                        ParentAscii = p.tblTypeArticle2.NameAscii
                    }).ToList();

            return list;
        }

        public List<ArticleTypeItem> GetAllChildOfType(int[] parent, bool isShow = true, bool isDeleted = false)
        {
            var list = new List<ArticleTypeItem>();

            var query = web365db.Database.SqlQuery<ArticleTypeItem>("EXEC [dbo].[PRC_AllChildTypeNewsByArrID] {0}", string.Join(",", parent));

            list = query.Select(p => new ArticleTypeItem()
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

        public List<ArticleTypeItem> GetAllChildOfTypeWithGroup(int groupId)
        {
            var list = new List<ArticleTypeItem>();

            var listId = web365db.tblGroup_TypeArticle_Map.Where(t => t.GroupTypeID == groupId && t.tblTypeArticle.LanguageId == LanguageId).Select(t => t.ArticleTypeID).ToArray();

            var query = web365db.Database.SqlQuery<ArticleTypeItem>("EXEC [dbo].[PRC_AllChildTypeNewsByArrID] {0}", string.Join(",", listId));

            list = query.Select(p => new ArticleTypeItem()
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

        public List<ArticleTypeItem> GetAllChildOfTypeAscii(string[] parent, bool isShow = true, bool isDeleted = false)
        {
            var list = new List<ArticleTypeItem>();

            var query = web365db.Database.SqlQuery<ArticleTypeItem>("EXEC [dbo].[PRC_AllChildTypeNews] {0}", string.Join(",", parent));

            list = query.Select(p => new ArticleTypeItem()
            {
                ID = p.ID,
                Parent = p.Parent,
                Name = p.Name,
                NameAscii = p.NameAscii,
                Number = p.Number,
                IsShow = p.IsShow,
                UrlPicture = p.UrlPicture
            }).ToList();

            return list;
        }
    }
}
