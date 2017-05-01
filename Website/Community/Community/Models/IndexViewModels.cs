using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Community.Models
{
    public class ContactModel
    {
        [Required]
        [Display(Name = "Name")]
        [MinLength(4, ErrorMessage = "Name must have a minimum of 4 characters.")]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        [EmailAddress]
        [System.ComponentModel.DataAnnotations.Compare("Email", ErrorMessage = "Emails do not match.")]
        [Display(Name = "Confirm Email")]
        public string ConfirmEmail { get; set; }
        [Required]
        [AllowHtml]
        public string Message { get; set; }
    }
}
