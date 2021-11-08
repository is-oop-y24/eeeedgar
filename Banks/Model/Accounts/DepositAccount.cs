using System;
using Banks.Model.Entities;

namespace Banks.Model.Accounts
{
    public class DepositAccount : IBankAccount
    {
        private BankClient _bankClient;
        private decimal _balance;
        private DateTime _creationDate;
        private DateTime _releaseDate;
        private BankingConditions _conditions;
        private decimal _expectedCharge;

        public DepositAccount(BankClient bankClient, DateTime creationDate, DateTime releaseDate, BankingConditions conditions)
        {
            _bankClient = bankClient;
            _balance = 0;
            _creationDate = creationDate;
            _releaseDate = releaseDate;
            _conditions = conditions;
            _expectedCharge = 0;
        }

        public decimal Interest => _conditions.DepositInterest.Interest(_balance);
        public DateTime CreationDate => _creationDate;
        public DateTime ReleaseDate => _releaseDate;
        public decimal ExpectedCharge => _expectedCharge;

        public decimal Balance()
        {
            return _balance;
        }

        public void DeductFunds(decimal money)
        {
            if (DateTime.Now < _releaseDate)
                throw new Exception("can't send money before the release date");
            _balance -= money;
        }

        public void DailyRenew(DateTime currentDate)
        {
            int daysInYear = DateTime.IsLeapYear(currentDate.Year) ? 366 : 365;

            // ReSharper disable once PossibleLossOfFraction
            _expectedCharge += _balance * (Interest / 100) / daysInYear;
            if (currentDate.Day == _creationDate.Day && currentDate != _creationDate)
            {
                _balance += _expectedCharge;
                _expectedCharge = 0;
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