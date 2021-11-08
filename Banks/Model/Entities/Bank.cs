using System;
using System.Collections.Generic;
using System.Linq;
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

        public string Name { get; init; }
        public BankingConditions Conditions { get; init; }
        public Dictionary<int, IBankAccount> BankAccounts { get; }

        public DepositAccount CreateDepositAccount(BankClient bankClient)
        {
            var depositAccount =
                new DepositAccount(bankClient, DateTime.Now, DateTime.Now + TimeSpan.FromDays(DateTime.IsLeapYear(DateTime.Now.Year) ? 366 : 355), Conditions);
            BankAccounts.Add(_nextAccountId++, depositAccount);
            return depositAccount;
        }

        public DebitAccount CreateDebitAccount(BankClient bankClient)
        {
            var debitAccount =
                new DebitAccount(bankClient, Conditions, DateTime.Now);
            BankAccounts.Add(_nextAccountId++, debitAccount);
            return debitAccount;
        }

        public CreditAccount CreateCreditAccount(BankClient bankClient)
        {
            var creditAccount =
                new CreditAccount(bankClient, DateTime.Now, Conditions);
            BankAccounts.Add(_nextAccountId++, creditAccount);
            return creditAccount;
        }

        public void DailyRenew(DateTime currentDate)
        {
            foreach ((int _, IBankAccount bankAccount) in BankAccounts)
            {
                bankAccount.DailyRenew(currentDate);
            }
        }

        public void SetNewDepositInterest(DepositInterest depositInterest)
        {
            Conditions.DepositInterest = depositInterest;
            foreach ((int _, IBankAccount bankAccount) in BankAccounts)
            {
                if (bankAccount.GetType() == typeof(DepositAccount))
                {
                    if (bankAccount.BankClient().IsSubscribed)
                        bankAccount.NotifyClient();
                }
            }
        }

        public void SetNewDebitInterest(decimal debitInterest)
        {
            Conditions.DebitInterest = debitInterest;
            foreach ((int _, IBankAccount bankAccount) in BankAccounts)
            {
                if (bankAccount.GetType() == typeof(DebitAccount))
                {
                    if (bankAccount.BankClient().IsSubscribed)
                        bankAccount.NotifyClient();
                }
            }
        }

        public void SetNewCreditLimit(decimal creditLimit)
        {
            Conditions.CreditLimit = creditLimit;
            foreach ((int _, IBankAccount bankAccount) in BankAccounts)
            {
                if (bankAccount.GetType() == typeof(CreditAccount))
                {
                    if (bankAccount.BankClient().IsSubscribed)
                        bankAccount.NotifyClient();
                }
            }
        }

        public void SetNewCreditCommission(decimal creditCommission)
        {
            Conditions.CreditCommission = creditCommission;
            foreach ((int _, IBankAccount bankAccount) in BankAccounts)
            {
                if (bankAccount.GetType() == typeof(CreditAccount))
                {
                    if (bankAccount.BankClient().IsSubscribed)
                        bankAccount.NotifyClient();
                }
            }
        }

        public void SetNewDoubtfulAccountLimit(decimal doubtfulAccountLimit)
        {
            Conditions.DoubtfulAccountLimit = doubtfulAccountLimit;
            foreach ((int _, IBankAccount bankAccount) in BankAccounts)
            {
                if (!bankAccount.IsConfirmed())
                    bankAccount.NotifyClient();
            }
        }
    }
}