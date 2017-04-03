using Community.Models;

namespace Community.Helpers
{
    class TransactionHelper
    {
        public static void CreateEventTrans(string senderID, string recipientID, int eventID, short amount)
        {
            VolunteerEntities db = new VolunteerEntities();

            Transaction transaction = new Transaction();
            transaction.Amount = amount;
            transaction.EventID = eventID;
            transaction.SenderID = senderID;
            transaction.RecipientID = recipientID;

            db.Transactions.Add(transaction);
            db.SaveChanges();
        }
    }
}
