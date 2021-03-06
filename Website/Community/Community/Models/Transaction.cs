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
    
    public partial class Transaction
    {
        public int ID { get; set; }
        public System.DateTime Date { get; set; }
        public string SenderID { get; set; }
        public string RecipientID { get; set; }
        public Nullable<int> EventID { get; set; }
        public bool Gift { get; set; }
        public short Amount { get; set; }
        public bool Cancelled { get; set; }
        public bool Complete { get; set; }
        public Nullable<int> ParentTransaction { get; set; }
    
        public virtual Event Event { get; set; }
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
    }
}
