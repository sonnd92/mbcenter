//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Web365Base
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblProduct_Attribute_Map
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Nullable<int> ProductID { get; set; }
        public Nullable<int> AttributeID { get; set; }
    
        public virtual tblProduct tblProduct { get; set; }
        public virtual tblProductAttribute tblProductAttribute { get; set; }
    }
}
