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
    
    public partial class tblBooking
    {
        public int Id { get; set; }
        public Nullable<int> Age { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public Nullable<System.DateTime> DateUpdated { get; set; }
        public Nullable<bool> Gender { get; set; }
        public string Address { get; set; }
        public Nullable<System.DateTime> BookDate { get; set; }
        public string Note { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<bool> IsViewed { get; set; }
        public int TypeBookingId { get; set; }
    
        public virtual tblBookingType tblBookingType { get; set; }
    }
}