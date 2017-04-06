using Community.Models;
using System;
using System.Linq;
using System.Web.Http.ModelBinding;

namespace Community.Helpers
{
    public class TransactionHelper
    {
        public static bool CreateEventTrans(string senderID, string recipientID, int eventID, short amount)
        {
            CommunityEntities db = new CommunityEntities();

            Transaction transaction = new Transaction();
            transaction.Amount = amount;
            transaction.EventID = eventID;
            transaction.SenderID = senderID;
            transaction.RecipientID = recipientID;
            transaction.Gift = false;
            transaction.Complete = false;
            transaction.Cancelled = false;
            transaction.Date = DateTime.Now;

            db.Transactions.Add(transaction);
            db.SaveChanges();
            db.reduceSenderBalance(transaction.ID);
            return true;
        }

        public static int GetVolunteerTransactionID(int eventID, string recipient, string sender) {
            CommunityEntities db = new CommunityEntities();

            var result = db.Transactions
                .Where(t => t.EventID == eventID 
                    && t.RecipientID == recipient
                    && t.SenderID == sender
                    && t.Gift == false)
                .Single();
            return result.ID;
        }
    }
}
