using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web365Domain.Order
{
    public class BookingItem : BaseModel
    {
        public int? Age { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public bool? Gender { get; set; } //true is male and false is female
        public string Address { get; set; }
        public DateTime? BookDate { get; set; }
        public string Note { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsViewed { get; set; }
        public int TypeBooking { get; set; }
    }
}
