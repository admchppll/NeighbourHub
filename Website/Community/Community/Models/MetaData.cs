using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Community.Models
{
    public class AddressMetaData
    {
        [Required]
        public string UserID { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "Name cannot be longer than 20 characters.")]
        public string Name { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Address line 1 cannot be longer than 50 characters.")]
        public string Address1 { get; set; }
        [StringLength(50, ErrorMessage = "Address line 2 cannot be longer than 50 characters.")]
        public string Address2 { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "City cannot be longer than 50 characters.")]
        public string City { get; set; }
        [StringLength(50, ErrorMessage = "County cannot be longer than 50 characters.")]
        public string County { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Country cannot be longer than 50 characters.")]
        public string Country { get; set; }
        [Required]
        [StringLength(15, ErrorMessage = "Postcode cannot be longer than 50 characters.")]
        public string Postcode { get; set; }
        [Required]
        public bool Default { get; set; }
    }

    public class AuditMetaData
    {
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime Date { get; set; }
        [Required]
        public string UserID { get; set; }
        [Required]
        [AllowHtml]
        public string AuditMessage { get; set; }
    }

    public class ContactMetaData
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [AllowHtml]
        [Required]
        public string Message { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public bool Replied { get; set; }

        public System.DateTime Date { get; set; }
    }

    public class EventMetaData
    {
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
        [Required]
        [Range(1, 12, ErrorMessage = "An event needs to be between 1 and 12 hours in length.")]
        public byte Length { get; set; }
        [StringLength(200, ErrorMessage = "Date Information cannot be longer than 200 characters.")]
        public string DateInfo { get; set; }
        [Required]
        public short VolunteerQuantity { get; set; }
        [Required]
        public short Points { get; set; }
    }

    public class InformationMetaData {
        [Required]
        [MinLength(4, ErrorMessage = "Label must be at least 4 characters long.")]
        [StringLength(50, ErrorMessage = "Label cannot be longer than 50 characters.")]
        public string Label { get; set; }
        [Required]
        [AllowHtml]
        public string Data { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime Edited { get; set; }
    }

    public class MessageMetaData {
        [Required]
        public string SenderID { get; set; }
        [Required]
        public string RecipientID { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Title cannot be longer than 50 characters.")]
        public string Title { get; set; }
        [Required]
        [AllowHtml]
        public string Body { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> Sent { get; set; }
    }

    public class NotificationMetaData {
        [Required]
        public string UserID { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "Title cannot be longer than 20 characters.")]
        public string Title { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Description cannot be longer than 50 characters.")]
        public string Description { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "Link cannot be longer than 200 characters.")]
        public string Link { get; set; }
    }

    public class OrganisationMetaData {
        [Required]
        [StringLength(50, ErrorMessage = "Organisation name cannot be longer than 200 characters.")]
        public string Name { get; set; }
        [Required]
        [AllowHtml]
        public string Description { get; set; }
        public short Balance { get; set; }
        [Required]
        [EmailAddress]
        [StringLength(100, ErrorMessage = "Email cannot be longer than 100 characters.")]
        public string Email { get; set; }
        [RegularExpression("^[0-9 ]*$", ErrorMessage = "Phone number can only contain numbers and spaces.")]
        [StringLength(20, ErrorMessage = "Phone number cannot be longer than 20 digits.")]
        public string Phone { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Address line 1 cannot be longer than 50 characters.")]
        public string Address1 { get; set; }
        [StringLength(50, ErrorMessage = "Address line 2 cannot be longer than 50 characters.")]
        public string Address2 { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "City cannot be longer than 50 characters.")]
        public string City { get; set; }
        [StringLength(50, ErrorMessage = "County cannot be longer than 50 characters.")]
        public string County { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Country cannot be longer than 50 characters.")]
        public string Country { get; set; }
        [Required]
        [StringLength(15, ErrorMessage = "Postcode cannot be longer than 15 characters.")]
        public string Postcode { get; set; }
        [StringLength(200, ErrorMessage = "Facebook link cannot be longer than 200 characters.")]
        public string Facebook { get; set; }
        [StringLength(200, ErrorMessage = "Twitter link cannot be longer than 200 characters.")]
        public string Twitter { get; set; }
        [StringLength(200, ErrorMessage = "Google link cannot be longer than 200 characters.")]
        public string Google { get; set; }
        [StringLength(200, ErrorMessage = "Youtube link cannot be longer than 200 characters.")]
        public string Youtube { get; set; }
        [Required]
        [RegularExpression("^[0-9 ]*$", ErrorMessage = "Charity number can only contain numbers and spaces.")]
        [StringLength(50, ErrorMessage = "Charity number cannot be longer than 50 characters.")]
        public string CharityNumber { get; set; }
        [Required]
        public bool Approved { get; set; }
        [Required]
        public bool Active { get; set; }
    }

    public class PointMetaData {
        [Required]
        public string UserID { get; set; }
        [Required]
        [Range(1, 28, ErrorMessage = "Day of month must be between 1 and 28")]
        public byte DayOfMonth { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime Expiry { get; set; }
    }

    public class ProfileMetaData {
        [Required]
        [StringLength(5, ErrorMessage = "Title cannot be longer than 5 characters.")]
        public string Title { get; set; }
        [Required]
        [Display(Name ="First Name")]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        [MinLength(2, ErrorMessage = "First name must contain at least 2 characters ")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Surname cannot be longer than 50 characters.")]
        [MinLength(2, ErrorMessage = "Surname must contain at least 2 characters ")]
        public string Surname { get; set; }
        [Required]
        public bool Gender { get; set; }
        [Required]
        [Display(Name = "Date of Birth")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }
        [Display(Name = "Phone Number")]
        [RegularExpression("^[0-9 ]*$", ErrorMessage = "Phone number can only contain numbers and spaces.")]
        [StringLength(20, ErrorMessage = "Phone number cannot be longer than 20 digits.")]
        public string Phone { get; set; }
        [AllowHtml]
        public string Biography { get; set; }
        [Display(Name ="Profile Image")]
        public string PictureURL { get; set; }
        [Required]
        public bool Active { get; set; }
        [Required]
        public bool Suspended { get; set; }
    }

    public class ReportMetaData {
        [Required]
        public string ReportedID { get; set; }
        [Required]
        [AllowHtml]
        public string Description { get; set; }
        [Display(Name = "Reported Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime Sent { get; set; }
    }

    public class SkillMetaData {
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime Created { get; set; }
    }

    public class TagMetaData
    {
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> Created { get; set; }
    }

    public class TransactionMetaData {
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime Date { get; set; }
        public string SenderID { get; set; }
        [Required]
        public string RecipientID { get; set; }
        public Nullable<int> EventID { get; set; }
        public bool Gift { get; set; }
        [Required]
        public short Amount { get; set; }
        public bool Cancelled { get; set; }
        [Required]
        public bool Complete { get; set; }
    }

    public class UserOrganisationMetaData {
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime Created { get; set; }
    }

    public class VolunteerMetaData {
        [Required]
        public int EventID { get; set; }
        [Required]
        public string VolunteerID { get; set; }
        [Required]
        public bool Accepted { get; set; }
        [Required]
        public bool Confirmed { get; set; }
        [Required]
        public bool Rejected { get; set; }
        [Required]
        public bool Withdrawn { get; set; }
    }
}
