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
    public class ProductDAFERepository : BaseFE, IProductDAFERepository
    {
        public tblProduct GetById(int id)
        {
            var query = from p in web365db.tblProduct
                        where p.ID == id
                        orderby p.ID descending
                        select p;
            return query.FirstOrDefault();
        }

        public ProductItem GetItemById(int id)
        {
            var product = new ProductItem();

            var result = GetById(id);

            product = new ProductItem()
            {
                ID = result.ID,
                TypeID = result.TypeID,
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
                IsShow = result.IsShow
            };

            return product;
        }

        public ProductItem GetItemByAscii(string ascii)
        {
            var product = new ProductItem();

            var result = (from p in web365db.tblProduct
                          where p.NameAscii == ascii && p.IsShow == true && p.IsDeleted == false
                          orderby p.ID descending
                          select p).FirstOrDefault();

            product = new ProductItem()
            {
                ID = result.ID,
                TypeID = result.TypeID,
                Name = result.Name,
                NameAscii = result.NameAscii,
                SEOTitle = result.SEOTitle,
                SEODescription = result.SEODescription,
                SEOKeyword = result.SEOKeyword,
                DateCreated = result.DateCreated,
                DateUpdated = result.DateUpdated,
                Number = result.Number,
                PictureID = result.PictureID,
                HighLights = result.HighLights,
                Summary = result.Summary,
                Price = result.Price,
                Detail = result.Detail,
                IsShow = result.IsShow,
                Picture = new PictureItem()
                {
                    FileName = result.tblPicture != null ? result.tblPicture.FileName : string.Empty
                },
                ListPicture = result.tblPicture1.Select(p => new PictureItem()
                {
                    FileName = p.FileName
                }).ToList(),
                ListFile = result.tblFile.Select(p => new FileItem()
                {
                    Name = p.Name,
                    FileName = p.FileName
                }).ToList()
            };

            return product;
        }

        public ProductModel SearchProduct(string keyword, int skip, int top)
        {
            var product = new ProductModel();

            var query = from p in web365db.tblProduct
                        where p.NameAscii.Contains(keyword) && p.tblTypeProduct.LanguageId == LanguageId && p.IsShow == true && p.IsDeleted == false
                        orderby p.Number descending, p.ID descending
                        select new ProductItem()
                        {
                            ID = p.ID,
                            TypeID = p.TypeID,
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
                            IsShow = p.IsShow,
                            Picture = new PictureItem()
                            {
                                FileName = p.tblPicture.FileName
                            },
                            ProductType = new ProductTypeItem()
                            {
                                Name = p.tblTypeProduct.Name,
                                NameAscii = p.tblTypeProduct.tblTypeProduct2.tblTypeProduct2.NameAscii + "/" + p.tblTypeProduct.tblTypeProduct2.NameAscii + "/" + p.tblTypeProduct.NameAscii
                            }
                        };

            product.Total = query.Count();

            product.ListProduct = query.Skip(skip).Take(top).ToList();

            if (product.Total > 0)
            {

                var listProductId = product.ListProduct.Select(p => p.ID).ToArray();

                var productVariants = (from p in web365db.tblProduct_Variant
                                       where listProductId.Contains(p.ProductID.Value)
                                       orderby p.DisplayOrder descending, p.Price ascending
                                       select new ProductVariantItem()
                                       {
                                           ID = p.ID,
                                           ProductID = p.ProductID,
                                           Name = p.Name,
                                           Price = p.Price.HasValue ? p.Price.Value : 0,
                                           IsOutOfStock = p.IsOutOfStock.HasValue ? p.IsOutOfStock.Value : false
                                       }).ToList();

                product.ListProduct.ForEach(p =>
                {
                    p.ListProductVariant = productVariants.Where(v => v.ProductID == p.ID).ToList();
                });
            }

            return product;
        }

        public ProductModel SearchProductAdvance(string asciiType, string asciiFilter, int skip, int top)
        {
            var product = new ProductModel();

            var arrTypeId = new int[] { };

            if (string.IsNullOrEmpty(asciiType))
            {
                arrTypeId = web365db.tblTypeProduct.Where(t => t.IsShow == true && t.IsDeleted == false).Select(t => t.ID).ToArray();
            }
            else
            {
                arrTypeId = web365db.tblTypeProduct.Where(t => t.NameAscii == asciiType && t.IsShow == true && t.IsDeleted == false).Select(t => t.ID).ToArray();
            }

            var query = from p in web365db.tblProduct
                        where p.tblProductFilter.Any(f => f.NameAscii == asciiFilter) && arrTypeId.Contains(p.TypeID.Value) && p.tblTypeProduct.LanguageId == LanguageId && p.IsShow == true && p.IsDeleted == false
                        orderby p.Number descending, p.ID descending
                        select new ProductItem()
                        {
                            ID = p.ID,
                            TypeID = p.TypeID,
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
                            IsShow = p.IsShow,
                            Picture = new PictureItem()
                            {
                                FileName = p.tblPicture.FileName
                            },
                            ProductType = new ProductTypeItem()
                            {
                                Name = p.tblTypeProduct.Name,
                                NameAscii = p.tblTypeProduct.tblTypeProduct2.tblTypeProduct2.NameAscii + "/" + p.tblTypeProduct.tblTypeProduct2.NameAscii + "/" + p.tblTypeProduct.NameAscii
                            }
                        };

            product.Total = query.Count();

            product.ListProduct = query.Skip(skip).Take(top).ToList();

            if (product.Total > 0)
            {

                var listProductId = product.ListProduct.Select(p => p.ID).ToArray();

                var productVariants = (from p in web365db.tblProduct_Variant
                                       where listProductId.Contains(p.ProductID.Value)
                                       orderby p.DisplayOrder descending, p.Price ascending
                                       select new ProductVariantItem()
                                       {
                                           ID = p.ID,
                                           ProductID = p.ProductID,
                                           Name = p.Name,
                                           Price = p.Price.HasValue ? p.Price.Value : 0,
                                           IsOutOfStock = p.IsOutOfStock.HasValue ? p.IsOutOfStock.Value : false
                                       }).ToList();

                product.ListProduct.ForEach(p =>
                {
                    p.ListProductVariant = productVariants.Where(v => v.ProductID == p.ID).ToList();
                });
            }

            return product;
        }

        public ProductModel GetListByTypeAscii(int skip, int top, string typeAscii)
        {
            var product = new ProductModel();

            var listType = web365db.tblTypeProduct.Where(t => t.NameAscii == typeAscii || t.tblTypeProduct2.NameAscii == typeAscii && t.LanguageId == LanguageId && t.IsShow == true && t.IsDeleted == false).Select(t => t.ID).ToArray();

            var query = from p in web365db.tblProduct
                        where listType.Contains(p.TypeID.Value) && p.tblTypeProduct.LanguageId == LanguageId && p.IsShow == true && p.IsDeleted == false
                        orderby p.Number descending, p.ID descending
                        select new ProductItem()
                        {
                            ID = p.ID,
                            TypeID = p.TypeID,
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
                            IsShow = p.IsShow,
                            Picture = new PictureItem()
                            {
                                FileName = p.tblPicture.FileName
                            },
                            ProductType = new ProductTypeItem()
                            {
                                Name = p.tblTypeProduct.Name,
                                NameAscii = p.tblTypeProduct.NameAscii
                            }
                        };

            product.Total = query.Count();

            product.ListProduct = query.Skip(skip).Take(top).ToList();

            if (product.Total > 0)
            {

                var listProductId = product.ListProduct.Select(p => p.ID).ToArray();

                var productVariants = (from p in web365db.tblProduct_Variant
                                       where listProductId.Contains(p.ProductID.Value)
                                       orderby p.DisplayOrder descending, p.Price ascending
                                       select new ProductVariantItem()
                                       {
                                           ID = p.ID,
                                           ProductID = p.ProductID,
                                           Name = p.Name,
                                           Price = p.Price.HasValue ? p.Price.Value : 0,
                                           IsOutOfStock = p.IsOutOfStock.HasValue ? p.IsOutOfStock.Value : false
                                       }).ToList();

                product.ListProduct.ForEach(p => {
                    p.ListProductVariant = productVariants.Where(v => v.ProductID == p.ID).ToList();
                });
            }

            return product;
        }
        
        public ProductModel GetListByType(int id, string ascii, int skip, int top)
        {
            var product = new ProductModel();

            var paramTotal = new SqlParameter
            {
                ParameterName = "Total",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };

            var query = web365db.Database.SqlQuery<ProductItem>("exec [dbo].[PRC_Product_GetProductByType] @TypeID, @TypeAscii, @Skip, @Top, @Total OUTPUT",
                           new SqlParameter("TypeID", id),
                           new SqlParameter("TypeAscii", ascii),
                           new SqlParameter("Skip", skip),
                           new SqlParameter("Top", top),
                           paramTotal);

            product.ListProduct = query.ToList();

            product.Total = Convert.ToInt32(paramTotal.Value);

            return product;
        }
        
        public ProductModel GetListProducts(int page, int pageSize, int orderBy)
        {
            var product = new ProductModel();

            var paramTotal = new SqlParameter
            {
                ParameterName = "Total",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };

            var query = web365db.Database.SqlQuery<ProductItem>("exec [dbo].[PRC_GetProducts] @Page, @PageSize, @OrderBy, @Total OUTPUT",
                           new SqlParameter("Page", page),
                           new SqlParameter("PageSize", pageSize),
                           new SqlParameter("OrderBy", orderBy),
                           paramTotal);

            product.ListProduct = query.ToList();

            product.Total = Convert.ToInt32(paramTotal.Value);

            return product;
        }

        public List<ProductItem> GetListOtherProducts(int prodId, int top)
        {
            var query = from c in web365db.tblProduct
                where c.IsDeleted == false && c.IsShow == true && c.ID != prodId
                select new ProductItem()
                {
                    ID = c.ID,
                    Name = c.Name,
                    Price = c.Price,
                    NameAscii = c.NameAscii,
                    Picture = new PictureItem()
                    {
                        FileName = c.tblPicture.FileName,
                        ID = c.tblPicture.ID
                    }
                };
            return query.OrderByDescending(c=>c.ID).Skip(0).Take(top).ToList();
        } 

        public ProductModel GetListByGroupId(int groupId)
        {
            var product = new ProductModel();

            var query = from p in web365db.tblProduct
                        where p.tblProduct_Status_Map.Any(s => s.tblProductStatus.ID == groupId) && p.tblTypeProduct.LanguageId == LanguageId && p.IsShow == true && p.IsDeleted == false
                        orderby p.Number descending, p.ID descending
                        select new ProductItem()
                        {
                            ID = p.ID,
                            TypeID = p.TypeID,
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
                            IsShow = p.IsShow,
                            Picture = new PictureItem()
                            {
                                FileName = p.tblPicture.FileName
                            },
                            ProductType = new ProductTypeItem()
                            {
                                Name = p.tblTypeProduct.Name,
                                NameAscii = p.tblTypeProduct.tblTypeProduct2.tblTypeProduct2.NameAscii + "/" + p.tblTypeProduct.tblTypeProduct2.NameAscii + "/" + p.tblTypeProduct.NameAscii
                            }
                        };

            product.Total = query.Count();

            product.ListProduct = query.ToList();

            if (product.Total > 0)
            {

                var listProductId = product.ListProduct.Select(p => p.ID).ToArray();

                var productVariants = (from p in web365db.tblProduct_Variant
                                       where listProductId.Contains(p.ProductID.Value)
                                       orderby p.DisplayOrder descending, p.Price ascending
                                       select new ProductVariantItem()
                                       {
                                           ID = p.ID,
                                           ProductID = p.ProductID,
                                           Name = p.Name,
                                           Price = p.Price.HasValue ? p.Price.Value : 0,
                                           IsOutOfStock = p.IsOutOfStock.HasValue ? p.IsOutOfStock.Value : false
                                       }).ToList();

                product.ListProduct.ForEach(p =>
                {
                    p.ListProductVariant = productVariants.Where(v => v.ProductID == p.ID).ToList();
                });
            }

            return product;
        }

        public ProductModel GetListByGroupAscii(string groupAscii)
        {
            var product = new ProductModel();

            var query = from p in web365db.tblProduct
                        where p.tblProduct_Status_Map.Any(s => s.tblProductStatus.NameAscii == groupAscii) && p.tblTypeProduct.LanguageId == LanguageId && p.IsShow == true && p.IsDeleted == false
                        orderby p.Number descending, p.ID descending
                        select new ProductItem()
                        {
                            ID = p.ID,
                            TypeID = p.TypeID,
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
                            IsShow = p.IsShow,
                            Picture = new PictureItem()
                            {
                                FileName = p.tblPicture.FileName
                            },
                            ProductType = new ProductTypeItem()
                            {
                                Name = p.tblTypeProduct.Name,
                                NameAscii = p.tblTypeProduct.tblTypeProduct2.tblTypeProduct2.NameAscii + "/" + p.tblTypeProduct.tblTypeProduct2.NameAscii + "/" + p.tblTypeProduct.NameAscii
                            }
                        };

            product.Total = query.Count();

            product.ListProduct = query.ToList();

            if (product.Total > 0)
            {

                var listProductId = product.ListProduct.Select(p => p.ID).ToArray();

                var productVariants = (from p in web365db.tblProduct_Variant
                                       where listProductId.Contains(p.ProductID.Value)
                                       orderby p.DisplayOrder descending, p.Price ascending
                                       select new ProductVariantItem()
                                       {
                                           ID = p.ID,
                                           ProductID = p.ProductID,
                                           Name = p.Name,
                                           Price = p.Price.HasValue ? p.Price.Value : 0,
                                           IsOutOfStock = p.IsOutOfStock.HasValue ? p.IsOutOfStock.Value : false
                                       }).ToList();

                product.ListProduct.ForEach(p =>
                {
                    p.ListProductVariant = productVariants.Where(v => v.ProductID == p.ID).ToList();
                });
            }

            return product;
        }
    }
}
