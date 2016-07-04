using System;

namespace Web365Domain.Service
{
    public class ServiceDetailItem : BaseModel
    {
        public string Name { get; set; }
        public string Detail { get; set; }
        public int? Price { get; set; }
        public bool? IsShow { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
    }
}
