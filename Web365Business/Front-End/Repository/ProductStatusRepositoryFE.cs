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
    public class ProductStatusRepositoryFE : BaseFE, IProductStatusRepositoryFE
    {

        private readonly IProductStatusDAFERepository productStatusDAFERepository;

        public ProductStatusRepositoryFE(IProductStatusDAFERepository _productStatusDAFERepository)
        {
            productStatusDAFERepository = _productStatusDAFERepository;
        }

        public ProductStatusItem GetByAscii(string ascii)
        {
            var key = string.Format("ProductStatusRepositoryFE{0}{1}", "GetByAscii", ascii);

            var item = new ProductStatusItem();

            var isDataCache = TryGetCache<ProductStatusItem>(out item, key);

            if (!isDataCache)
            {
                item = productStatusDAFERepository.GetByAscii(ascii);
            }

            SetCache(key, item, isDataCache, Web365Utility.ConfigCache.Cache10Minute);

            return item;
        }
    }
}
