using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Web365Base;
using Web365DA.RDBMS.Front_End.IRepository;
using Web365Domain;
using Web365Utility;

namespace Web365DA.RDBMS.Front_End.Repository
{
    public class ProductFilterDAFERepository : BaseFE, IProductFilterDAFERepository
    {
        public List<ProductFilterItem> GetByParent(int? parent)
        {
            var result = new List<ProductFilterItem>();

            var query = from p in web365db.tblProductFilter
                        where p.IsShow == true && p.IsDeleted == false
                        select p;

            if (parent.HasValue)
            {
                query = query.Where(p => p.Parent == parent);
            }
            else
            {
                query = query.Where(p => p.Parent == null);
            }

            result = query.OrderByDescending(p => p.Number).Select(p => new ProductFilterItem()
            {
                ID = p.ID,
                Name = p.Name,
                NameAscii = p.NameAscii,
                Number = p.Number
            }).ToList();

            return result;
        }
    }
}
