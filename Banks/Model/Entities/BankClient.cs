using System;
using Banks.Model.Tools;

namespace Banks.Model.Entities
{
    public class BankClient
    {
        public Guid Id { get; set; }
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public string Address { get; private set; }

        public string PassportData { get; private set; }

        public bool IsSubscribed { get; private set; }

        public static BankClient CreateInstance(string name, string surname)
        {
            return new BankClient
            {
                Id = Guid.NewGuid(),
                Name = name,
                Surname = surname,
            };
        }

        public BankClient SetAddress(string address)
        {
            if (Address != null)
                throw new BanksException("illegal attempt to set client's address");
            Address = address;
            return this;
        }

        public BankClient SetPassportData(string passportData)
        {
            if (PassportData != null)
                throw new BanksException("illegal attempt to set client's passport data");
            PassportData = passportData;
            return this;
        }

        public BankClient Subscribe()
        {
            IsSubscribed = true;
            return this;
        }

        public BankClient Unsubscribe()
        {
            IsSubscribed = false;
            return this;
        }
    }
}