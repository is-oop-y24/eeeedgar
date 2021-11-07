using System;
using Banks.Model.Entities;

namespace Banks.Model.Accounts
{
    public class DepositAccount : IBankAccount
    {
        private BankClient _bankClient;
        private decimal _balance;
        private DateTime _releaseTime;
        private DepositInterest _depositInterest;

        public DepositAccount(BankClient bankClient, DateTime releaseTime, DepositInterest depositInterest)
        {
            _bankClient = bankClient;
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

        public string StringType()
        {
            return GetType().ToString().Split('.')[^1];
        }
    }
}