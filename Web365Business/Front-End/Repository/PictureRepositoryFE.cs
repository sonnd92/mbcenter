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
    public class PictureRepositoryFE : BaseFE, IPictureRepositoryFE
    {
        private readonly IPictureDAFERepository pictureDAFERepository;

        public PictureRepositoryFE(IPictureDAFERepository _pictureDAFERepository)
        {
            pictureDAFERepository = _pictureDAFERepository;
        }

        public PictureModel GetListByType(int id, string ascii, int skip, int top)
        {
            var key = string.Format("PictureRepositoryFE{0}{1}", "GetListByType", id, ascii, skip, top);

            var item = new PictureModel();

            var isDataCache = TryGetCache<PictureModel>(out item, key);

            if (!isDataCache)
            {
                item = pictureDAFERepository.GetListByType(id, ascii, skip, top);
            }

            SetCache(key, item, isDataCache, Web365Utility.ConfigCache.Cache10Minute);

            return item;
        }
    }
}
