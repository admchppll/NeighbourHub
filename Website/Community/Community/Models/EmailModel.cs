using Postal;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Community.Models
{
    public class EmailModel : Email
    {
        public string To { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        [AllowHtml]
        public string Html { get; set; }
    }
}
