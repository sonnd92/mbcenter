using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Web365Base;
using Web365DA.RDBMS.Front_End.IRepository;
using Web365Domain;
using Web365Models;
using Web365Utility;

namespace Web365DA.RDBMS.Front_End.Repository
{
    public class ArticleDAFERepository : BaseFE, IArticleDAFERepository
    {
        public ArticleItem GetItemById(int id)
        {
            var article = new ArticleItem();

            var result = (from p in web365db.tblArticle
                          where p.ID == id
                          orderby p.ID descending
                          select p).FirstOrDefault();

            article = new ArticleItem()
            {
                ID = result.ID,
                TypeID = result.TypeID,
                Title = result.Title,
                TitleAscii = result.TitleAscii,
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
                Picture = new PictureItem()
                {
                    ID = result.tblPicture.ID,
                    FileName = result.tblPicture != null ? result.tblPicture.FileName : string.Empty
                }
            };

            return article;
        }

        public ArticleItem GetItemByNameAscii(string nameAscii, bool isShow = true, bool isDeleted = false)
        {
            var article = new ArticleItem();

            var result = (from p in web365db.tblArticle
                          where p.TitleAscii == nameAscii && p.tblTypeArticle.LanguageId == LanguageId && p.IsShow == isShow && p.IsDeleted == isDeleted
                          orderby p.ID descending
                          select p).FirstOrDefault();

            article = new ArticleItem()
            {
                ID = result.ID,
                TypeID = result.TypeID,
                Title = result.Title,
                TitleAscii = result.TitleAscii,
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
                ArticleType = new ArticleTypeItem()
                {
                    ID = result.tblTypeArticle.ID,
                    Name = result.tblTypeArticle.Name,
                    NameAscii = result.tblTypeArticle.NameAscii
                },
                ListPicture = result.tblPicture1.Select(p => new PictureItem()
                {
                    FileName = p.FileName
                }).ToList(),
                Picture = new PictureItem()
                {
                    ID = result.tblPicture.ID,
                    FileName = result.tblPicture.FileName
                }
            };

            return article;
        }

        public ListArticleModel GetListByType(int typeID, string nameAscii, int currentRecord, int pageSize, bool isShow = true, bool isDeleted = false)
        {
            var result = new ListArticleModel();

            var paramTotal = new SqlParameter
            {
                ParameterName = "Total",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };

            var query = web365db.Database.SqlQuery<ArticleMapItem>("exec [dbo].[PRC_GetListNewsByType] @TypeID, @TypeAscii, @Language, @CurrentRecord, @PageSize, @Total OUTPUT",
                new SqlParameter("TypeID", typeID),
                new SqlParameter("TypeAscii", nameAscii),
                new SqlParameter("Language", LanguageId),
                new SqlParameter("CurrentRecord", currentRecord),
                new SqlParameter("PageSize", pageSize),
                paramTotal);

            result.List = query.Select(p => new ArticleItem()
            {
                ID = p.ID,
                Title = p.Title,
                TitleAscii = p.TitleAscii,
                Summary = p.Summary,
                Picture = new PictureItem()
                {
                    FileName = p.PictureURL
                },
                ArticleType = new ArticleTypeItem()
                {
                    Name = p.TypeName,
                    NameAscii = p.TypeNameAscii,
                    ParentName = p.TypeParentName,
                    ParentAscii = p.TypeParentNameAscii
                },
                Detail = p.Detail,
                Number = p.Number
            }).OrderBy(p => p.Number).ToList();

            result.total = Convert.ToInt32(paramTotal.Value);

            return result;
        }

        public ListArticleModel GetListByArrType(int[] arrType, int currentRecord, int pageSize, bool isShow = true, bool isDeleted = false)
        {
            var result = new ListArticleModel();

            var query = from p in web365db.tblArticle
                        where arrType.Contains(p.TypeID.Value) && p.IsShow == isShow && p.IsDeleted == isDeleted
                        orderby p.Number descending, p.ID descending
                        select new ArticleItem()
                        {
                            ID = p.ID,
                            TypeID = p.TypeID,
                            Title = p.Title,
                            TitleAscii = p.TitleAscii,
                            SEOTitle = p.SEOTitle,
                            Summary = p.Summary,
                            Picture = new PictureItem()
                            {
                                ID = p.tblPicture.ID,
                                Name = p.tblPicture.Name,
                                FileName = p.tblPicture.FileName
                            },
                            ArticleType = new ArticleTypeItem()
                            {
                                ID = p.tblTypeArticle.ID,
                                Name = p.tblTypeArticle.Name,
                                NameAscii = p.tblTypeArticle.NameAscii,
                                SEOTitle = p.tblTypeArticle.SEOTitle,
                                SEODescription = p.tblTypeArticle.SEODescription,
                                SEOKeyword = p.tblTypeArticle.SEOKeyword
                            }
                        };

            result.total = query.Count();

            result.List = query.Skip(currentRecord).Take(pageSize).ToList();

            return result;
        }

        public List<ArticleItem> GetTopByType(int type, int skip, int top, bool isShow = true, bool isDeleted = false)
        {
            var list = new List<ArticleItem>();

            list = (from p in web365db.tblArticle
                    where p.TypeID == type && p.IsShow == isShow && p.IsDeleted == isDeleted
                    orderby p.ID descending
                    select new ArticleItem()
                    {
                        ID = p.ID,
                        TypeID = p.TypeID,
                        Title = p.Title,
                        TitleAscii = p.TitleAscii,
                        SEOTitle = p.SEOTitle,
                        Summary = p.Summary,
                        Picture = new PictureItem()
                        {
                            ID = p.tblPicture.ID,
                            Name = p.tblPicture.Name,
                            FileName = p.tblPicture.FileName
                        },
                        ArticleType = new ArticleTypeItem()
                        {
                            ID = p.tblTypeArticle.ID,
                            Name = p.tblTypeArticle.Name,
                            NameAscii = p.tblTypeArticle.NameAscii,
                            SEOTitle = p.tblTypeArticle.SEOTitle,
                            SEODescription = p.tblTypeArticle.SEODescription,
                            SEOKeyword = p.tblTypeArticle.SEOKeyword
                        },
                        Detail = p.Detail
                    }).Skip(skip).Take(top).ToList();

            return list;
        }

        public ListArticleModel GetNewestNews(int typeID, int currentRecord, int pageSize, bool isShow = true, bool isDeleted = false)
        {
            var result = new ListArticleModel();
            var list = new List<ArticleItem>();

            list = (from p in web365db.tblArticle
                    where p.TypeID == typeID && p.IsShow == isShow && p.IsDeleted == isDeleted
                    orderby p.ID descending
                    select new ArticleItem()
                    {
                        ID = p.ID,
                        TypeID = p.TypeID,
                        Title = p.Title,
                        TitleAscii = p.TitleAscii,
                        SEOTitle = p.SEOTitle,
                        Summary = p.Summary,
                        Picture = new PictureItem()
                        {
                            ID = p.tblPicture.ID,
                            Name = p.tblPicture.Name,
                            FileName = p.tblPicture.FileName
                        },
                        Detail = p.Detail
                    }).ToList();
            result.total = list.Count;
            result.List = list.Skip(currentRecord).Take(pageSize).ToList();

            return result;
        }

        public List<ArticleItem> GetOtherArticle(int type, int articleId, int skip, int top, bool isShow = true, bool isDeleted = false)
        {
            var list = new List<ArticleItem>();

            list = (from p in web365db.tblArticle
                    where p.TypeID == type && p.ID < articleId && p.IsShow == isShow && p.IsDeleted == isDeleted
                    orderby p.ID descending
                    select new ArticleItem()
                    {
                        ID = p.ID,
                        TypeID = p.TypeID,
                        Title = p.Title,
                        TitleAscii = p.TitleAscii,
                        SEOTitle = p.SEOTitle,
                        Picture = new PictureItem()
                        {
                            ID = p.tblPicture.ID,
                            Name = p.tblPicture.Name,
                            FileName = p.tblPicture.FileName
                        },
                        ArticleType = new ArticleTypeItem()
                        {
                            ID = p.tblTypeArticle.ID,
                            Name = p.tblTypeArticle.Name,
                            NameAscii = p.tblTypeArticle.NameAscii,
                            ParentName = p.tblTypeArticle.tblTypeArticle2.Name,
                            ParentAscii = p.tblTypeArticle.tblTypeArticle2.NameAscii,
                            SEOTitle = p.tblTypeArticle.SEOTitle,
                            SEODescription = p.tblTypeArticle.SEODescription,
                            SEOKeyword = p.tblTypeArticle.SEOKeyword
                        }
                    }).Skip(skip).Take(top).ToList();

            return list;
        }

        public ListArticleModel ArticleSeach(string[] keyword, string[] keywordAscii, int currentRecord, int top, bool isShow = true, bool isDeleted = false)
        {
            var list = new ListArticleModel();

            var query = (from p in web365db.tblArticle
                         where (keyword.All(k => p.Title.ToLower().Contains(k)) || keyword.All(k => p.TitleAscii.Contains(k))) && p.TypeID > 1 && p.tblTypeArticle.LanguageId == LanguageId && p.IsShow == isShow && p.IsDeleted == isDeleted
                         orderby p.ID descending
                         select new ArticleItem()
                         {
                             ID = p.ID,
                             Title = p.Title,
                             TitleAscii = p.TitleAscii,
                             SEOTitle = p.SEOTitle,
                             Summary = p.Summary,
                             Picture = new PictureItem()
                             {
                                 Name = p.tblPicture != null ? p.tblPicture.Name : string.Empty,
                                 FileName = p.tblPicture != null ? p.tblPicture.FileName : string.Empty
                             },
                             ArticleType = new ArticleTypeItem()
                             {
                                 ID = p.tblTypeArticle != null ? p.tblTypeArticle.ID : 0,
                                 Name = p.tblTypeArticle.Name,
                                 NameAscii = p.tblTypeArticle.NameAscii,
                                 ParentAscii = p.tblTypeArticle.tblTypeArticle2.NameAscii
                             }
                         });

            list.total = query.Count();

            list.List = query.Skip(currentRecord).Take(top).ToList();

            return list;
        }

        public ListArticleModel GetListByGroup(int groupId, int skip, int top, bool isShow = true, bool isDeleted = false)
        {
            var list = new ListArticleModel();

            var query = (from p in web365db.tblGroup_Article_Map
                         where p.GroupID == groupId && p.tblArticle.tblTypeArticle.LanguageId == LanguageId && p.tblArticle.IsShow == isShow && p.tblArticle.IsDeleted == isDeleted
                         orderby p.ID descending
                         select new ArticleItem()
                         {
                             ID = p.tblArticle.ID,
                             TypeID = p.tblArticle.TypeID,
                             Title = p.tblArticle.Title,
                             TitleAscii = p.tblArticle.TitleAscii,
                             SEOTitle = p.tblArticle.SEOTitle,
                             Summary = p.tblArticle.Summary,
                             Picture = new PictureItem()
                             {
                                 ID = p.tblArticle.tblPicture.ID,
                                 Name = p.tblArticle.tblPicture.Name,
                                 FileName = p.tblArticle.tblPicture.FileName
                             },
                             ArticleType = new ArticleTypeItem()
                             {
                                 ID = p.tblArticle.tblTypeArticle.ID,
                                 Name = p.tblArticle.tblTypeArticle.Name,
                                 NameAscii = p.tblArticle.tblTypeArticle.NameAscii,
                                 ParentName = p.tblArticle.tblTypeArticle.tblTypeArticle2.Name,
                                 ParentAscii = p.tblArticle.tblTypeArticle.tblTypeArticle2.NameAscii,
                             }
                         });

            list.total = query.Count();

            list.List = query.Skip(skip).Take(top).ToList();

            return list;
        }

        public ListArticleModel GetListByTypeAndDetail(int typeId, int skip, int top, bool isShow = true, bool isDeleted = false)
        {
            var list = new ListArticleModel();

            var query = (from p in web365db.tblArticle
                         where p.TypeID == typeId && p.IsShow == isShow && p.IsDeleted == isDeleted
                         orderby p.Number descending, p.ID ascending
                         select new ArticleItem()
                         {
                             ID = p.ID,
                             TypeID = p.TypeID,
                             Title = p.Title,
                             TitleAscii = p.TitleAscii,
                             SEOTitle = p.SEOTitle,
                             Summary = p.Summary,
                             Detail = p.Detail,
                             Picture = new PictureItem()
                             {
                                 ID = p.tblPicture != null ? p.tblPicture.ID : 0,
                                 Name = p.tblPicture.Name,
                                 FileName = p.tblPicture.FileName
                             },
                             ArticleType = new ArticleTypeItem()
                             {
                                 ID = p.tblTypeArticle.ID,
                                 Name = p.tblTypeArticle.Name,
                                 NameAscii = p.tblTypeArticle.NameAscii,
                                 ParentAscii = p.tblTypeArticle.tblTypeArticle2.NameAscii,
                                 ParentName = p.tblTypeArticle.tblTypeArticle2.Name
                             }
                         });

            list.total = query.Count();

            list.List = query.Skip(skip).Take(top).ToList();

            return list;
        }
    }
}
