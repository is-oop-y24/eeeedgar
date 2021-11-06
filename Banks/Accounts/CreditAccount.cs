using System;

namespace Banks.Accounts
{
    public class CreditAccount : IBankAccount
    {
        private decimal _balance;
        private decimal _creditLimit;
        private decimal _commission;
        public CreditAccount(int creditLimit, int commission)
        {
            _balance = 0;
            _creditLimit = creditLimit;
            _commission = commission;
        }

        public decimal Balance()
        {
            return _balance;
        }

        public void SendMoney(decimal money)
        {
            if (_balance - money < -_creditLimit)
                throw new Exception("going under the credit limit");
            if (_balance < 0)
            {
                if (_balance - money - _commission < -_creditLimit)
                    throw new Exception("going under the credit limit");
                _balance -= money + _commission;
            }
            else
            {
                if (_balance - money < -_creditLimit)
                    throw new Exception("going under the credit limit");
                _balance -= money;
            }

            _balance -= money;
        }

        public void ScheduleRenew(decimal t)
        {
            if (_balance < 0)
                _balance -= _commission * Math.Floor(t / 365);
        }

        public void ReceiveMoney(decimal money)
        {
            _balance += money;
        }
    }
}