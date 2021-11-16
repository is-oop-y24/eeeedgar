using System;

namespace Banks.Model.Accounts
{
    public class CreditAccount : BankAccount
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

        public override void DeductFunds(decimal money)
        {
            if (Balance - money < -BankingConditions.CreditLimit)
                throw new Exception("going under the credit limit");
            Balance -= money;
        }

        public override void DailyRenew(DateTime currentDate)
        {
            if (currentDate.Day == CreationDate.Day && Balance < 0)
            {
                Balance -= BankingConditions.CreditCommission;
            }
        }

        public override void CreditFunds(decimal money)
        {
            Balance += money;
        }
    }
}