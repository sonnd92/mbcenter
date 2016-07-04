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
    public class PictureTypeDAFERepository : BaseFE, IPictureTypeDAFERepository
    {
        public List<PictureTypeItem> GetAllChildOfType(int[] parent, bool isShow = true, bool isDeleted = false)
        {
            var query = web365db.Database.SqlQuery<PictureTypeItem>("EXEC [dbo].[PRC_AllChildTypePicureByArrID] {0}", string.Join(",", parent));
            
            return query.Select(p => new PictureTypeItem()
            {
                ID = p.ID,
                Parent = p.Parent,
                Name = p.Name,
                NameAscii = p.NameAscii,
                Number = p.Number,
                IsShow = p.IsShow
            }).ToList();
        }
    }
}
