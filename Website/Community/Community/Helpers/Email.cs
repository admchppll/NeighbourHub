using Community.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Helpers
{
    public class EmailHelper
    {
        public static void Create(string To, string Subject, string Message)
        {
            EmailModel email = new EmailModel();
            email.To = To;
            email.From = ConfigurationManager.AppSettings["DefaultEmail"];
            email.Subject = Subject;
            email.Message = Message;
            email.Html = Message;
            email.Send();
        }

        public static void Create(string To, string Subject, string Message, string Html)
        {
            EmailModel email = new EmailModel();
            email.To = To;
            email.From = ConfigurationManager.AppSettings["DefaultEmail"];
            email.Subject = Subject;
            email.Message = Message;
            email.Html = Html;
            email.Send();
        }

        public static void UserCreate(string UserID, string Subject, string Message)
        {
            CommunityEntities db = new CommunityEntities();
            var user = db.Users.Find(UserID);
            EmailModel email = new EmailModel();
            email.To = user.Email;
            email.From = ConfigurationManager.AppSettings["DefaultEmail"];
            email.Subject = Subject;
            email.Message = Message;
            email.Html = Message;
            email.Send();
        }

        public static void UserCreate(string UserID, string Subject, string Message, string Html)
        {
            CommunityEntities db = new CommunityEntities();
            var user = db.Users.Find(UserID);
            EmailModel email = new EmailModel();
            email.To = user.Email;
            email.From = ConfigurationManager.AppSettings["DefaultEmail"];
            email.Subject = Subject;
            email.Message = Message;
            email.Html = Html;
            email.Send();
        }
    }
}
