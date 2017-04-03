using Community.Models;

namespace Community.Helpers
{
    class TransactionHelper
    {
        public static Transaction CreateEventTrans(string senderID, string recipientID, int eventID, short amount)
        {
            Transaction transaction = new Transaction();
            transaction.Amount = amount;
            transaction.EventID = eventID;
            transaction.SenderID = senderID;
            transaction.RecipientID = recipientID;

            return transaction;
        }
    }
}
