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
    
    public partial class Message
    {
        public int ID { get; set; }
        public string SenderID { get; set; }
        public string RecipientID { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public Nullable<System.DateTime> Sent { get; set; }
        public bool Read { get; set; }
        public bool Saved { get; set; }
        public Nullable<bool> Admin { get; set; }
        public Nullable<int> ParentMessage { get; set; }
    
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
    }
}
