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
    public class PictureDAFERepository : BaseFE, IPictureDAFERepository
    {
        public PictureModel GetListByType(int id, string ascii, int skip, int top)
        {
            var picture = new PictureModel();

            var paramTotal = new SqlParameter
            {
                ParameterName = "Total",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };

            var query = web365db.Database.SqlQuery<PictureItem>("exec [dbo].[PRC_Picture_GetPictureByType] @TypeID, @TypeAscii, @Skip, @Top, @Total OUTPUT",
                           new SqlParameter("TypeID", id),
                           new SqlParameter("TypeAscii", ascii),
                           new SqlParameter("Skip", skip),
                           new SqlParameter("Top", top),
                           paramTotal);

            picture.List = query.ToList();

            picture.Total = Convert.ToInt32(paramTotal.Value);

            return picture;
        }
    }
}
