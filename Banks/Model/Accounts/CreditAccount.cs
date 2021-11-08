using System;
using Banks.Model.Entities;

namespace Banks.Model.Accounts
{
    public class CreditAccount : IBankAccount
    {
        private BankClient _bankClient;
        private decimal _balance;
        private DateTime _creationDate;
        private BankingConditions _conditions;
        public CreditAccount(BankClient bankClient, DateTime creationDate, BankingConditions conditions)
        {
            _bankClient = bankClient;
            _creationDate = creationDate;
            _conditions = conditions;
            _balance = 0;
        }

        public decimal Balance()
        {
            return _balance;
        }

        public void DeductFunds(decimal money)
        {
            if (_balance - money < -_conditions.CreditLimit)
                throw new Exception("going under the credit limit");
            _balance -= money;
        }

        public void DailyRenew(DateTime currentDate)
        {
            if (currentDate.Day == _creationDate.Day && _balance < 0)
            {
                _balance -= _conditions.CreditCommission;
            }
        }

        public void CreditFunds(decimal money)
        {
            _balance += money;
        }

        public string StringType()
        {
            return GetType().ToString().Split('.')[^1];
        }

        public bool IsConfirmed()
        {
            return _bankClient.IsCompleted;
        }

        public BankClient BankClient() => _bankClient;
        public BankingConditions BankingConditions()
        {
            return _conditions;
        }

        public void NotifyClient()
        {
            _bankClient.ReceiveNotification();
        }
    }
}