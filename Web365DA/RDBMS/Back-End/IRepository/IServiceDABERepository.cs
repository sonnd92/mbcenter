using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web365Domain.Service;

namespace Web365DA.RDBMS.Back_End.IRepository
{
    public interface IServiceDABERepository : IBaseDABERepository
    {
        List<ServiceItem> GetList(out int total, string name, int currentRecord, int numberRecord,
            string propertyNameSort, bool descending, bool isDelete = false);
    }
}
