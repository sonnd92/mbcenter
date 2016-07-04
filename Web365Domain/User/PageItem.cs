﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Web365Domain
{
    public class PageItem : BaseModel
    {
        public string Name { get; set; }
        public string Link { get; set; }
        public string ClassAtrtibute { get; set; }
        public int? Parent { get; set; }
        public bool? HasChild { get; set; }
        public int Number { get; set; }
        public string Detail { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsShow { get; set; }
        public bool IsDeleted { get; set; }
    }
}
