using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web365Domain.Service
{
    public class ServiceItem : BaseModel
    {
        public string Title { get; set; }
        public string Name { get; set; }
        public string NameAscii { get; set; }
        public string Summary { get; set; }
        public string TitleAscii { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsShow { get; set; }
        public bool? IsFeartured { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public string CreateBy { get; set; }
        public PictureItem Picture { get; set; }
        public int? Index { get; set; }
        public int? PictureId { get; set; }
        public string SEOTitle { get; set; }
        public string SEODescription { get; set; }
        public string SEOKeyword { get; set; }

        public List<StepOfServiceItem> StepItems { get; set; }
        public List<GroupOfServiceItem> GroupOfServiceItems { get; set; }
    }
}
