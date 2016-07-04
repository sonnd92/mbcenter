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
    public class FileRepositoryFE : BaseFE, IFileRepositoryFE
    {
        private readonly IFileDAFERepository fileDAFERepository;

        public FileRepositoryFE(IFileDAFERepository _fileDAFERepository)
        {
            fileDAFERepository = _fileDAFERepository;
        }

        public FileModel GetListByType(int id, string ascii, int skip, int top)
        {
            var key = string.Format("FileRepositoryFE{0}{1}{2}{3}{4}", "GetListByType", id, ascii, skip, top);

            var item = new FileModel();

            var isDataCache = TryGetCache<FileModel>(out item, key);

            if (!isDataCache)
            {
                item = fileDAFERepository.GetListByType(id, ascii, skip, top);
            }

            SetCache(key, item, isDataCache, Web365Utility.ConfigCache.Cache10Minute);

            return item;
        }
    }
}
