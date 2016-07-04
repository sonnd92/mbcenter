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
    public class ProductFilterRepositoryFE : BaseFE, IProductFilterRepositoryFE
    {
        private readonly IProductFilterDAFERepository productFilterDAFERepository;

        public ProductFilterRepositoryFE(IProductFilterDAFERepository _productFilterDAFERepository)
        {
            productFilterDAFERepository = _productFilterDAFERepository;
        }

        public List<ProductFilterItem> GetByParent(int? parent)
        {
            var key = string.Format("ProductFilterRepositoryFE{0}{1}", "GetByParent", string.Join("_", parent));

            var item = new List<ProductFilterItem>();

            var isDataCache = TryGetCache<List<ProductFilterItem>>(out item, key);

            if (!isDataCache)
            {
                item = productFilterDAFERepository.GetByParent(parent);
            }

            SetCache(key, item, isDataCache, Web365Utility.ConfigCache.Cache10Minute);

            return item;
        }
    }
}
