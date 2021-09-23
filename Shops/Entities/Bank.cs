using System;
using System.Collections.Generic;
using System.Linq;

namespace Shops.Entities
{
    public class Bank : IBank
    {
        private List<BankProfile> _profiles;

        private Bank()
        {
            _profiles = new List<BankProfile>();
        }

        public static Bank CreateInstance()
        {
            return new Bank();
        }

        public bool MakeTransaction(int senderId, int recipientId, int transactionValue)
        {
            BankProfile senderProfile = GetProfile(senderId);
            BankProfile recipientProfile = GetProfile(recipientId);
            if (senderProfile.Balance < transactionValue)
                return false;

            senderProfile.Balance -= transactionValue;
            recipientProfile.Balance += transactionValue;
            return true;
        }

        public void RegisterProfile(BankClient bankClient)
        {
            _profiles.Add(BankProfile.CreateInstance(bankClient));
        }

        public int ProfileBalance(int id)
        {
            return (from profile in _profiles where profile.BankClient.Id == id select profile.Balance).FirstOrDefault();
        }

        public bool HasProfile(int id)
        {
            return _profiles.Find(profile => profile.BankClient.Id == id) != null;
        }

        private BankProfile GetProfile(int id)
        {
            BankProfile profile = _profiles.Find(prof => prof.BankClient.Id == id);
            if (profile == null)
                throw new Exception("wrong profile id");
            return profile;
        }
    }
}
