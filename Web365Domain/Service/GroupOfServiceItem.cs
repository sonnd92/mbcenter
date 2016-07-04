using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web365Base;

namespace Web365Domain.Service
{
    public class GroupOfServiceItem : BaseModel
    {
        public string Name { get; set; }
        public string Summary { get; set; }
        public byte[] NameAscii { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsShow { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public int? ServiceId { get; set; }

        public List<DetailServiceItem> DetailServiceItems { get; set; }
    }
}
