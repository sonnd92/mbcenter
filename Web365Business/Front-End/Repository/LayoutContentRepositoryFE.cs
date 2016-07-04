﻿using System;
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
    public class LayoutContentRepositoryFE : BaseFE, ILayoutContentRepositoryFE
    {
        private readonly ILayoutContentDAFERepository layoutContentDAFERepository;

        public LayoutContentRepositoryFE(ILayoutContentDAFERepository _layoutContentDAFERepository)
        {
            layoutContentDAFERepository = _layoutContentDAFERepository;
        }

        public LayoutContentItem GetItemById(int id)
        {
            var key = string.Format("FileRepositoryFE{0}{1}", "GetItemById", id);

            var item = new LayoutContentItem();

            var isDataCache = TryGetCache<LayoutContentItem>(out item, key);

            if (!isDataCache)
            {
                item = layoutContentDAFERepository.GetItemById(id);
            }

            SetCache(key, item, isDataCache, Web365Utility.ConfigCache.Cache10Minute);

            return item;
        }
    }
}
