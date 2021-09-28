using System.Collections.Generic;
using Shops.Tools;

namespace Shops.Entities
{
    public class Bank : IBank
    {
        private const int BonusForNewClients = 100;
        private List<BankProfile> _profiles;

        public Bank()
        {
            _profiles = new List<BankProfile>();
        }

        public void RegisterProfile(BankClient bankClient, int balance = BonusForNewClients)
        {
            _profiles.Add(BankProfile.CreateInstance(bankClient, balance));
        }

        public int ProfileBalance(int clientId)
        {
            BankProfile profile = GetProfile(clientId);
            return profile.Balance;
        }

        public void MakeTransaction(int senderId, int recipientId, int transactionValue)
        {
            BankProfile senderProfile = GetProfile(senderId);
            BankProfile recipientProfile = GetProfile(recipientId);
            if (senderProfile.Balance < transactionValue)
                throw new ShopException("insufficient funds for the operation");

            senderProfile.Balance -= transactionValue;
            recipientProfile.Balance += transactionValue;
        }

        private BankProfile GetProfile(int id)
        {
            return _profiles.Find(prof => prof.BankClient.Id == id) ?? throw new ShopException("wrong profile id");
        }
    }
}
