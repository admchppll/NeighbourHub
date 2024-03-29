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
    
    public partial class Event
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Event()
        {
            this.Audits = new HashSet<Audit>();
            this.EventTags = new HashSet<EventTag>();
            this.Reports = new HashSet<Report>();
            this.Transactions = new HashSet<Transaction>();
            this.Volunteers = new HashSet<Volunteer>();
            this.Reviews = new HashSet<Review>();
            this.Bookmarks = new HashSet<Bookmark>();
        }
    
        public int ID { get; set; }
        public string HostID { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public int AddressID { get; set; }
        public System.DateTime Created { get; set; }
        public bool Published { get; set; }
        public Nullable<System.DateTime> Edited { get; set; }
        public System.DateTime Date { get; set; }
        public System.TimeSpan Time { get; set; }
        public Nullable<short> Repeated { get; set; }
        public Nullable<byte> RepeatIncrement { get; set; }
        public byte Length { get; set; }
        public bool AM1 { get; set; }
        public bool AM2 { get; set; }
        public bool AM3 { get; set; }
        public bool AM4 { get; set; }
        public bool AM5 { get; set; }
        public bool AM6 { get; set; }
        public bool AM7 { get; set; }
        public bool PM1 { get; set; }
        public bool PM2 { get; set; }
        public bool PM3 { get; set; }
        public bool PM4 { get; set; }
        public bool PM5 { get; set; }
        public bool PM6 { get; set; }
        public bool PM7 { get; set; }
        public string DateInfo { get; set; }
        public bool Suspended { get; set; }
        public short VolunteerQuantity { get; set; }
        public short Points { get; set; }
        public string PictureURL { get; set; }
        public Nullable<bool> Cancelled { get; set; }
    
        public virtual Address Address { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Audit> Audits { get; set; }
        public virtual User User { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EventTag> EventTags { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Report> Reports { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Transaction> Transactions { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Volunteer> Volunteers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Review> Reviews { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bookmark> Bookmarks { get; set; }
    }
}
