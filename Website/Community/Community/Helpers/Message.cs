using Community.Models;
using System.Linq;
using System.Data;

namespace Community.Helpers
{
    public class MessageHelper
    {
        public static int GetUnreadMessages(string userID) {
            CommunityEntities db = new CommunityEntities();

            int count = db.Messages
                .Where(m => m.RecipientID == userID 
                    && m.Read == false 
                    && m.Sent != null)
                .Count();

            return count;
        }

        public static int GetAllMessages(string userID)
        {
            CommunityEntities db = new CommunityEntities();

            int count = db.Messages
                .Where(m => (m.RecipientID == userID && m.Sent != null)
                    || m.SenderID == userID)
                .Count();

            return count;
        }

        public static int GetAllSentMessages(string userID)
        {
            CommunityEntities db = new CommunityEntities();

            int count = db.Messages
                .Where(m => m.SenderID == userID 
                    && m.Sent != null)
                .Count();

            return count;
        }

        public static int GetAllReceivedMessages(string userID)
        {
            CommunityEntities db = new CommunityEntities();

            int count = db.Messages
                .Where(m => m.RecipientID == userID
                    && m.Sent != null)
                .Count();

            return count;
        }

        public static int GetAllSavedMessages(string userID)
        {
            CommunityEntities db = new CommunityEntities();

            int count = db.Messages
                .Where(m => m.SenderID == userID
                    && m.Sent == null)
                .Count();

            return count;
        }

        public static void SetRead(string userID, int messageID)
        {
            CommunityEntities db = new CommunityEntities();
            Message message = db.Messages.Find(messageID);
            message.Read = true;

            db.SaveChanges();
        }
    }
}
