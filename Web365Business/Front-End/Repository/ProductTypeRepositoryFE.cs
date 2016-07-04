using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Web365Base;
using Web365Business.Front_End.IRepository;
using Web365DA.RDBMS.Front_End.IRepository;
using Web365Domain;
using Web365Utility;

namespace Web365Business.Front_End.Repository
{
    public class ProductTypeRepositoryFE : BaseFE, IProductTypeRepositoryFE
    {
        private readonly IProductTypeDAFERepository productTypeDAFERepository;

        public ProductTypeRepositoryFE(IProductTypeDAFERepository _productTypeDAFERepository)
        {
            productTypeDAFERepository = _productTypeDAFERepository;
        }

        public ProductTypeItem GetByParentAsciiAndAscii(string parentAscii, string ascii)
        {
            var key = string.Format("ProductTypeRepositoryFE{0}{1}{2}{3}", "GetByParentAsciiAndAscii", parentAscii, ascii);

            var item = new ProductTypeItem();

            var isDataCache = TryGetCache<ProductTypeItem>(out item, key);

            if (!isDataCache)
            {
                item = productTypeDAFERepository.GetByParentAsciiAndAscii(parentAscii, ascii);
            }

            SetCache(key, item, isDataCache, Web365Utility.ConfigCache.Cache10Minute);

            return item;
        }

        public ProductTypeItem GetItemById(int id)
        {
            var key = string.Format("ProductTypeRepositoryFE{0}{1}", "GetItemById", id);

            var item = new ProductTypeItem();

            var isDataCache = TryGetCache<ProductTypeItem>(out item, key);

            if (!isDataCache)
            {
                item = productTypeDAFERepository.GetItemById(id);
            }

            SetCache(key, item, isDataCache, Web365Utility.ConfigCache.Cache10Minute);

            return item;
        }

        public ProductTypeItem GetByAsciiAndParent(string ascii, int parentId)
        {
            var key = string.Format("ProductTypeRepositoryFE{0}{1}{2}", "GetByAsciiAndParent", ascii, parentId);

            var item = new ProductTypeItem();

            var isDataCache = TryGetCache<ProductTypeItem>(out item, key);

            if (!isDataCache)
            {
                item = productTypeDAFERepository.GetByAsciiAndParent(ascii, parentId);
            }

            SetCache(key, item, isDataCache, Web365Utility.ConfigCache.Cache10Minute);

            return item;
        }

        public List<ProductTypeItem> GetListByParent(int? parentId)
        {
            var key = string.Format("ProductTypeRepositoryFE{0}{1}", "GetListByParent", parentId);

            var item = new List<ProductTypeItem>();

            var isDataCache = TryGetCache<List<ProductTypeItem>>(out item, key);

            if (!isDataCache)
            {
                item = productTypeDAFERepository.GetListByParent(parentId);
            }

            SetCache(key, item, isDataCache, Web365Utility.ConfigCache.Cache10Minute);

            return item;
        }

        public List<ProductTypeItem> GetAllChildByParent(int? parentId)
        {
            var key = string.Format("ProductTypeRepositoryFE{0}{1}", "GetAllChildByParent", parentId);

            var item = new List<ProductTypeItem>();

            var isDataCache = TryGetCache<List<ProductTypeItem>>(out item, key);

            if (!isDataCache)
            {
                item = productTypeDAFERepository.GetAllChildByParent(parentId);
            }

            SetCache(key, item, isDataCache, Web365Utility.ConfigCache.Cache10Minute);

            return item;
        }

        public List<ProductTypeItem> GetByGroup(int groupId)
        {
            var key = string.Format("ProductTypeRepositoryFE{0}{1}", "GetByGroup", groupId);

            var item = new List<ProductTypeItem>();

            var isDataCache = TryGetCache<List<ProductTypeItem>>(out item, key);

            if (!isDataCache)
            {
                item = productTypeDAFERepository.GetByGroup(groupId);
            }

            SetCache(key, item, isDataCache, Web365Utility.ConfigCache.Cache10Minute);

            return item;
        }
    }
}
