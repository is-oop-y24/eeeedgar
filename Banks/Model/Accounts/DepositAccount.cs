using System;

namespace Banks.Model.Accounts
{
    public class DepositAccount : BankAccount
    {
        private decimal _initialBalance;
        public override decimal InitialBalance
        {
            get => _initialBalance;

            init
            {
                _initialBalance = value;
                Balance = InitialBalance;
            }
        }

        public decimal Interest => BankingConditions.DepositInterest.Count(_initialBalance);
        public DateTime ReleaseDate { get; init; }

        public decimal ExpectedCharge { get; private set; }

        public override void DeductFunds(decimal money)
        {
            if (DateTime.Now < ReleaseDate)
                throw new Exception("can't send money before the release date");
            Balance -= money;
        }

        public override void DailyRenew(DateTime currentDate)
        {
            ExpectedCharge += Balance * BankingConditions.DailyInterest(Interest, currentDate);
            if (currentDate.Day == CreationDate.Day && currentDate != CreationDate)
            {
                Balance += ExpectedCharge;
                ExpectedCharge = 0;
            }
        }

        public override void CreditFunds(decimal money)
        {
            Balance += money;
        }
    }
}