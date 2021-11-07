using System;
using Banks.Model.Entities;

namespace Banks.Model.Accounts
{
    public class DebitAccount : IBankAccount
    {
        private BankClient _bankClient;
        private decimal _balance;
        private decimal _interest;
        public DebitAccount(BankClient bankClient, decimal interest)
        {
            _bankClient = bankClient;
            _balance = 0;
            _interest = interest;
        }

        public decimal Interest => _interest;

        public decimal Balance()
        {
            return _balance;
        }

        public void SendMoney(decimal money)
        {
            if (_balance - money < 0)
                throw new Exception("debit account balance can't be < 0");
            _balance -= money;
        }

        public void ReceiveMoney(decimal money)
        {
            _balance += money;
        }

        public string StringType()
        {
            return GetType().ToString().Split('.')[^1];
        }

        public void ScheduleRenew(decimal t)
        {
            _balance *= 1 + ((_interest / 100) * (t / 365));
        }
    }
}