using System;

namespace Banks.Model.Accounts
{
    public class DepositAccount : BankAccount
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

        public decimal Interest { get; init; }
        public DateTime ReleaseDate { get; set; }

        public decimal ExpectedCharge { get; private set; }

        public override void DeductFunds(decimal money)
        {
            if (DateTime.Now < ReleaseDate)
                throw new Exception("can't send money before the release date");
            Balance -= money;
        }

        public override void DailyRenew(DateTime currentDate)
        {
            int daysInYear = DateTime.IsLeapYear(currentDate.Year) ? 366 : 365;

            ExpectedCharge += Balance * (Interest / 100) / daysInYear;
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

        public override string StringType()
        {
            return GetType().ToString().Split('.')[^1];
        }
    }
}