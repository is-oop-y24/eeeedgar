using System;
using Banks.Model.Entities;

namespace Banks.Model.Accounts
{
    public class DebitAccount : IBankAccount
    {
        private BankClient _bankClient;
        private decimal _balance;
        private BankingConditions _conditions;
        private DateTime _creationDate;
        private decimal _expectedCharge;
        public DebitAccount(BankClient bankClient, BankingConditions conditions, DateTime creationDate)
        {
            _bankClient = bankClient;
            _balance = 0;
            _conditions = conditions;
            _creationDate = creationDate;
            _expectedCharge = 0;
        }

        public decimal Interest => _conditions.DebitInterest;
        public DateTime CreationDate => _creationDate;
        public decimal ExpectedCharge => _expectedCharge;

        public decimal Balance()
        {
            return _balance;
        }

        public void DeductFunds(decimal money)
        {
            if (_balance - money < 0)
                throw new Exception("debit account balance can't be < 0");
            _balance -= money;
        }

        public void CreditFunds(decimal money)
        {
            _balance += money;
        }

        public string StringType()
        {
            return GetType().ToString().Split('.')[^1];
        }

        public void DailyRenew(DateTime currentDate)
        {
            int daysInYear = DateTime.IsLeapYear(currentDate.Year) ? 366 : 365;
            _expectedCharge += _balance * Interest / 100 / daysInYear;
            if (currentDate.Day == _creationDate.Day && currentDate != _creationDate)
            {
                _balance += _expectedCharge;
                _expectedCharge = 0;
            }
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