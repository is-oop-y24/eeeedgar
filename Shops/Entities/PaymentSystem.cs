using System.Collections.Generic;
using Shops.Tools;

namespace Shops.Entities
{
    public class PaymentSystem
    {
        private static List<PaymentSystemUserProfile> _profiles;

        public static void RegisterProfile(PaymentSystemUser paymentSystemUser, int balance = 0)
        {
            _profiles ??= new List<PaymentSystemUserProfile>();
            _profiles.Add(new PaymentSystemUserProfile(paymentSystemUser, balance));
        }

        public static int ProfileBalance(int clientId)
        {
            _profiles ??= new List<PaymentSystemUserProfile>();
            PaymentSystemUserProfile profile = GetProfile(clientId);
            return profile.Balance;
        }

        public static void MakeTransaction(int senderId, int recipientId, int transactionValue)
        {
            _profiles ??= new List<PaymentSystemUserProfile>();
            PaymentSystemUserProfile senderProfile = GetProfile(senderId);
            PaymentSystemUserProfile recipientProfile = GetProfile(recipientId);
            if (senderProfile.Balance < transactionValue)
                throw new ShopException("insufficient funds for the operation");

            senderProfile.Balance -= transactionValue;
            recipientProfile.Balance += transactionValue;
        }

        private static PaymentSystemUserProfile GetProfile(int id)
        {
            _profiles ??= new List<PaymentSystemUserProfile>();
            return _profiles.Find(prof => prof.PaymentSystemUser.Id == id) ?? throw new ShopException("wrong profile id");
        }
    }
}
