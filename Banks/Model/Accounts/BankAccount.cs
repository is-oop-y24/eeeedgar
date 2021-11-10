using System;
using Banks.Model.Entities;

namespace Banks.Model.Accounts
{
    public abstract class BankAccount
    {
        public Guid Id { get; set; }
        public decimal Balance { get; protected set; }
        public abstract decimal InitialBalance { get; init; }
        public BankClient BankClient { get; set; }
        public BankingConditions BankingConditions { get; set; }
        public DateTime CreationDate { get; set; }

        public abstract void DeductFunds(decimal money);

        public abstract void DailyRenew(DateTime currentDate);

        public abstract void CreditFunds(decimal money);
        public abstract string StringType();

        public bool IsConfirmed()
        {
            return BankClient.Address != null && BankClient.PassportData != null;
        }

        public void NotifyClient()
        {
        }
    }
}