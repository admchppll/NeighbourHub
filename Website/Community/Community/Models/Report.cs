//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Community.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Report
    {
        public int ID { get; set; }
        public string UserID { get; set; }
        public Nullable<int> ReportedEvent { get; set; }
        public string ReportedID { get; set; }
        public string Description { get; set; }
    
        public virtual Event Event { get; set; }
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
    }
}
