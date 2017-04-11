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
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    public partial class Event
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Event()
        {
            this.Audits = new HashSet<Audit>();
            this.Bookmarkeds = new HashSet<Bookmarked>();
            this.EventTags = new HashSet<EventTag>();
            this.Reports = new HashSet<Report>();
            this.Transactions = new HashSet<Transaction>();
            this.Volunteers = new HashSet<Volunteer>();
        }
    
        public int ID { get; set; }
        public string HostID { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Title cannot be longer than 100 characters.")]
        public string Title { get; set; }
        [StringLength(200, ErrorMessage = "Surname cannot be longer than 200 characters.")]
        public string ShortDescription { get; set; }
        [Required]
        [AllowHtml]
        public string LongDescription { get; set; }
        [Required]
        public int AddressID { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime Created { get; set; }
        [Required]
        public bool Published { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> Edited { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime Date { get; set; }
        [Required(ErrorMessage = "Time is a required field in the format HH:MM (24 hour time) E.g. 14:15")]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        public System.TimeSpan Time { get; set; }
        public Nullable<short> Repeated { get; set; }
        public Nullable<byte> RepeatIncrement { get; set; }
        [Required]
        [Range(1,12, ErrorMessage = "An event needs to be between 1 and 12 hours in length.")]
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
        [StringLength(200, ErrorMessage = "Date Information cannot be longer than 200 characters.")]
        public string DateInfo { get; set; }
        public bool Suspended { get; set; }
        [Required]
        public short VolunteerQuantity { get; set; }
        [Required]
        public short Points { get; set; }
        public string PictureURL { get; set; }
    
        public virtual Address Address { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Audit> Audits { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bookmarked> Bookmarkeds { get; set; }
        public virtual User User { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EventTag> EventTags { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Report> Reports { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Transaction> Transactions { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Volunteer> Volunteers { get; set; }
    }
}
