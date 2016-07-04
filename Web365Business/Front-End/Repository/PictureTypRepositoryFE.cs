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
    public class PictureTypRepositoryFE : BaseFE, IPictureTypRepositoryFE
    {
        private readonly IPictureTypeDAFERepository pictureTypeDAFERepository;

        public PictureTypRepositoryFE(IPictureTypeDAFERepository _pictureTypeDAFERepository)
        {
            pictureTypeDAFERepository = _pictureTypeDAFERepository;
        }

        public List<PictureTypeItem> GetAllChildOfType(int[] parent, bool isShow = true, bool isDeleted = false)
        {
            var key = string.Format("PictureTypRepositoryFE{0}{1}{}{}", "GetAllChildOfType", string.Join(",", parent), isShow, isDeleted);

            var item = new List<PictureTypeItem>();

            var isDataCache = TryGetCache<List<PictureTypeItem>>(out item, key);

            if (!isDataCache)
            {
                item = pictureTypeDAFERepository.GetAllChildOfType(parent, isShow, isDeleted);
            }

            SetCache(key, item, isDataCache, Web365Utility.ConfigCache.Cache10Minute);

            return item;
        }
    }
}
