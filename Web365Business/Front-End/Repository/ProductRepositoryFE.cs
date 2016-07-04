using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Web365Base;
using Web365Business.Front_End.IRepository;
using Web365DA.RDBMS.Front_End.IRepository;
using Web365Domain;
using Web365Models;
using Web365Utility;

namespace Web365Business.Front_End.Repository
{
    public class ProductRepositoryFE : BaseFE, IProductRepositoryFE
    {
        private readonly IProductDAFERepository productDAFERepository;

        public ProductRepositoryFE(IProductDAFERepository _productDAFERepository)
        {
            productDAFERepository = _productDAFERepository;
        }

        public ProductItem GetItemById(int id)
        {
            var key = string.Format("ProductRepositoryFE{0}{1}", "GetItemById", id);

            var item = new ProductItem();

            var isDataCache = TryGetCache<ProductItem>(out item, key);

            if (!isDataCache)
            {
                item = productDAFERepository.GetItemById(id);
            }

            SetCache(key, item, isDataCache, Web365Utility.ConfigCache.Cache10Minute);

            return item;
        }

        public ProductItem GetItemByAscii(string ascii)
        {
            var key = string.Format("ProductRepositoryFE{0}{1}", "GetItemByAscii", ascii);

            var item = new ProductItem();

            var isDataCache = TryGetCache<ProductItem>(out item, key);

            if (!isDataCache)
            {
                item = productDAFERepository.GetItemByAscii(ascii);
            }

            SetCache(key, item, isDataCache, Web365Utility.ConfigCache.Cache10Minute);

            return item;
        }

        public ProductModel SearchProduct(string keyword, int skip, int top)
        {
            var key = string.Format("ProductRepositoryFE{0}{1}{2}{3}", "SearchProduct", keyword, skip, top);

            var item = new ProductModel();

            var isDataCache = TryGetCache<ProductModel>(out item, key);

            if (!isDataCache)
            {
                item = productDAFERepository.SearchProduct(keyword, skip, top);
            }

            SetCache(key, item, isDataCache, Web365Utility.ConfigCache.Cache10Minute);

            return item;
        }

        public ProductModel SearchProductAdvance(string asciiType, string asciiFilter, int skip, int top)
        {
            var key = string.Format("ProductRepositoryFE{0}{1}{2}{3}{4}", "SearchProductAdvance", asciiType, asciiFilter, skip, top);

            var item = new ProductModel();

            var isDataCache = TryGetCache<ProductModel>(out item, key);

            if (!isDataCache)
            {
                item = productDAFERepository.SearchProductAdvance(asciiType, asciiFilter, skip, top);
            }

            SetCache(key, item, isDataCache, Web365Utility.ConfigCache.Cache10Minute);

            return item;
        }

        public ProductModel GetListByTypeAscii(int skip, int top, string typeAscii)
        {
            var key = string.Format("ProductRepositoryFE{0}{1}{2}{3}", "GetListByTypeAscii", typeAscii, skip, top);

            var item = new ProductModel();

            var isDataCache = TryGetCache<ProductModel>(out item, key);

            if (!isDataCache)
            {
                item = productDAFERepository.GetListByTypeAscii(skip, top, typeAscii);
            }

            SetCache(key, item, isDataCache, Web365Utility.ConfigCache.Cache10Minute);

            return item;
        }
        
        public ProductModel GetListByType(int id, string ascii, int skip, int top)
        {
            var key = string.Format("ProductRepositoryFE{0}{1}{2}{3}{4}", "GetListByType", id, ascii, skip, top);

            var item = new ProductModel();

            var isDataCache = TryGetCache<ProductModel>(out item, key);

            if (!isDataCache)
            {
                item = productDAFERepository.GetListByType(id, ascii, skip, top);
            }

            SetCache(key, item, isDataCache, Web365Utility.ConfigCache.Cache10Minute);

            return item;
        }

        public ProductModel GetListProducts(int page, int pageSize, int orderBy)
        {
            var key = string.Format("ProductRepositoryFE{0}{1}{2}{3}", "GetListProducts", page, pageSize, orderBy);

            var item = new ProductModel();

            var isDataCache = TryGetCache<ProductModel>(out item, key);

            if (!isDataCache)
            {
                item = productDAFERepository.GetListProducts(page, pageSize, orderBy);
            }

            SetCache(key, item, isDataCache, Web365Utility.ConfigCache.Cache10Minute);

            return item;
        }

        public List<ProductItem> GetListOtherProducts(int prodId, int top)
        {
            var key = string.Format("ProductRepositoryFE{0}{1}{2}", "GetListOtherProducts", prodId, top);

            var item = new List<ProductItem>();

            var isDataCache = TryGetCache<List<ProductItem>>(out item, key);

            if (!isDataCache)
            {
                item = productDAFERepository.GetListOtherProducts(prodId, top);
            }

            SetCache(key, item, isDataCache, Web365Utility.ConfigCache.Cache10Minute);

            return item;
        }

        public ProductModel GetListByGroupId(int groupId)
        {
            var key = string.Format("ProductRepositoryFE{0}{1}", "GetListByGroupId", groupId);

            var item = new ProductModel();

            var isDataCache = TryGetCache<ProductModel>(out item, key);

            if (!isDataCache)
            {
                item = productDAFERepository.GetListByGroupId(groupId);
            }

            SetCache(key, item, isDataCache, Web365Utility.ConfigCache.Cache10Minute);

            return item;
        }

        public ProductModel GetListByGroupAscii(string groupAscii)
        {
            var key = string.Format("ProductRepositoryFE{0}{1}", "GetListByGroupAscii", groupAscii);

            var item = new ProductModel();

            var isDataCache = TryGetCache<ProductModel>(out item, key);

            if (!isDataCache)
            {
                item = productDAFERepository.GetListByGroupAscii(groupAscii);
            }

            SetCache(key, item, isDataCache, Web365Utility.ConfigCache.Cache10Minute);

            return item;
        }
    }
}
