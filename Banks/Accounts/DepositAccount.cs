using System;

namespace Banks.Accounts
{
    public class DepositAccount : IBankAccount
    {
        private decimal _balance;
        private DateTime _releaseTime;
        private DepositInterest _depositInterest;

        public DepositAccount(DateTime releaseTime, DepositInterest depositInterest)
        {
            _balance = 0;
            _releaseTime = releaseTime;
            _depositInterest = depositInterest;
        }

        public decimal Balance()
        {
            return _balance;
        }

        public void SendMoney(decimal money)
        {
            if (DateTime.Now < _releaseTime)
                throw new Exception("can't send money before the release date");
        }

        public void ScheduleRenew(decimal t)
        {
            _balance *= 1 + ((_depositInterest.Interest(_balance) / 100) * (t / 365));
        }

        public void ReceiveMoney(decimal money)
        {
            _balance += money;
        }
    }
}