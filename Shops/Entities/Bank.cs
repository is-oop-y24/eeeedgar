using System;
using System.Collections.Generic;
using Shops.Services;

namespace Shops.Entities
{
    public class Bank
    {
        private List<BankProfile> _profiles;

        private Bank(ShopManager shopManager)
        {
            _profiles = new List<BankProfile>();
            ShopManager = shopManager;
        }

        public IReadOnlyList<BankProfile> Profiles => _profiles;
        public ShopManager ShopManager { get; }

        public static Bank CreateInstance(ShopManager shopManager)
        {
            return new Bank(shopManager);
        }

        public bool IsTransactionPossible(BankClient sender, int transactionValue)
        {
            BankProfile bankProfile = FindProfile(sender);
            return IsTransactionPossible(bankProfile, transactionValue);
        }

        public void MakeTransaction(int senderId, int recipientId, int transactionValue)
        {
            BankProfile senderProfile = FindProfile(senderId);
            BankProfile recipientProfile = FindProfile(recipientId);
            MakeTransaction(senderProfile, recipientProfile, transactionValue);
        }

        public void RegisterProfile(BankClient bankClient)
        {
            _profiles.Add(BankProfile.CreateInstance(bankClient));
        }

        public void GiveMoney(int id, int money)
        {
            BankProfile bankProfile = FindProfile(id);
            if (bankProfile != null)
            {
                bankProfile.Balance += money;
            }
        }

        private BankProfile FindProfile(int id)
        {
            return _profiles.Find(profile => profile.BankClient.Id == id);
        }

        private BankProfile FindProfile(BankClient bankClient)
        {
            return _profiles.Find(profile => profile.BankClient == bankClient);
        }

        private bool IsTransactionPossible(BankProfile sender, int transactionValue)
        {
            return sender.Balance >= transactionValue;
        }

        private void MakeTransaction(BankProfile sender, BankProfile recipient, int transactionValue)
        {
            sender.Balance -= transactionValue;
            if (sender.Balance < 0)
            {
                throw new Exception("invalig transaction commited");
            }

            recipient.Balance += transactionValue;
        }
    }
}
