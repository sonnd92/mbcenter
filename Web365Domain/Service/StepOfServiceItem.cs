using System;
using System.Collections.Generic;

namespace Web365Domain.Service
{
    public class StepOfServiceItem : BaseModel
    {
        public string Name { get; set; }
        public int? Step { get; set; }
        public string Course { get; set; }
        public string Uses { get; set; }
        public DateTime? DateCreated { get; set; }
        public bool? IsShow { get; set; }
        public bool? IsDeleted { get; set; }
        public string CreateBy { get; set; }
        public string UpdateBy { get; set; }
        public int? ServiceId { get; set; }

        public List<PictureItem> SlidesPictureItems { get; set; }
        public int[] ListPictureID { get; set; } 
    }
}
