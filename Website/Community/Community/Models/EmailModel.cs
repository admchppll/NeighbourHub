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

    public class EmailExtendedModel : Email
    {
        public string To { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }
        [AllowHtml]
        public string Message { get; set; }
        public string RecipientName { get; set; }
        public string SenderName { get; set; }
        public string ReplyEmail { get; set; }
    }
}
