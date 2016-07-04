﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web365Domain;

namespace Web365Models
{
    public class ListProductModel
    {
        public ProductStatusItem ProductStatus { get; set; }
        public ProductTypeItem TypeParent { get; set; }
        public ProductTypeItem Type { get; set; }
        public ProductModel ListProduct { get; set; }
        public List<ProductTypeItem> ListProductType { get; set; }
    }
}
