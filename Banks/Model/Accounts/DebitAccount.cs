using System;

namespace Banks.Model.Accounts
{
    public class DebitAccount : BankAccount
    {
        private decimal _initialBalance;
        public override decimal InitialBalance
        {
            get
            {
                return _initialBalance;
            }

            init
            {
                _initialBalance = value;
                Balance = InitialBalance;
            }
        }

        public decimal Interest => BankingConditions.DebitInterest;
        public decimal ExpectedCharge { get; private set; }

        public override void DeductFunds(decimal money)
        {
            if (Balance - money < 0)
                throw new Exception("debit account balance can't be < 0");
            Balance -= money;
        }

        public override void CreditFunds(decimal money)
        {
            Balance += money;
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
    }
}