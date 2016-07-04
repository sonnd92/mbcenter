using System;

namespace Web365Domain.Service
{
    public class DetailServiceItem : BaseModel
    {

        public string Name { get; set; }
        public string Detail { get; set; }
        public int? Price { get; set; }
        public bool? IsShow { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public int? GroupId { get; set; }
        public int? Index { get; set; }
    }
}
