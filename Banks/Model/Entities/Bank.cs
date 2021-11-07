using System;
using System.Collections.Generic;
using Banks.Model.Accounts;

namespace Banks.Model.Entities
{
    public class Bank
    {
        private int _nextAccountId;
        public Bank()
        {
            BankAccounts = new Dictionary<int, IBankAccount>();
        }

        public Dictionary<int, IBankAccount> BankAccounts { get; }
        public string Name { get; init; }
        public DepositInterest DepositInterest { get; init; }
        public decimal DebitInterest { get; init; }
        public decimal CreditLimit { get; init; }
        public decimal CreditCommission { get; init; }

        public DepositAccount CreateDepositAccount(BankClient bankClient)
        {
            var depositAccount =
                new DepositAccount(bankClient, DateTime.Now + TimeSpan.FromDays(365), DepositInterest);
            BankAccounts.Add(_nextAccountId++, depositAccount);
            return depositAccount;
        }

        public DebitAccount CreateDebitAccount(BankClient bankClient)
        {
            var debitAccount =
                new DebitAccount(bankClient, DebitInterest);
            BankAccounts.Add(_nextAccountId++, debitAccount);
            return debitAccount;
        }

        public CreditAccount CreateCreditAccount(BankClient bankClient)
        {
            var creditAccount =
                new CreditAccount(bankClient, CreditLimit, CreditCommission);
            BankAccounts.Add(_nextAccountId++, creditAccount);
            return creditAccount;
        }
    }
}