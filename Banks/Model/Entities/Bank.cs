using System;
using System.Collections.Generic;
using Banks.Model.Accounts;
using Banks.Model.Entities.DepositStuff;

namespace Banks.Model.Entities
{
    public class Bank
    {
        private Bank()
        {
            BankAccounts = new List<BankAccount>();
        }

        public Guid Id { get; set; }
        public string Name { get; private set; }
        public BankingConditions Conditions { get; private set; }
        public List<BankAccount> BankAccounts { get; }

        public static Bank CreateInstance(string name)
        {
            return new Bank
            {
                Id = Guid.NewGuid(),
                Name = name,
                Conditions = new BankingConditions(),
            };
        }

        public Bank SetDepositInterest(DepositInterest depositInterest)
        {
            Conditions.DepositInterest = depositInterest;
            foreach (BankAccount bankAccount in BankAccounts)
            {
                if (bankAccount.GetType() == typeof(DepositAccount))
                {
                    if (bankAccount.BankClient.IsSubscribed)
                        bankAccount.NotifyClient();
                }
            }

            return this;
        }

        public Bank SetDebitInterest(decimal debitInterest)
        {
            Conditions.DebitInterest = debitInterest;
            foreach (BankAccount bankAccount in BankAccounts)
            {
                if (bankAccount.GetType() == typeof(DebitAccount))
                {
                    if (bankAccount.BankClient.IsSubscribed)
                        bankAccount.NotifyClient();
                }
            }

            return this;
        }

        public Bank SetCreditLimit(decimal creditLimit)
        {
            Conditions.CreditLimit = creditLimit;
            foreach (BankAccount bankAccount in BankAccounts)
            {
                if (bankAccount.GetType() == typeof(CreditAccount))
                {
                    if (bankAccount.BankClient.IsSubscribed)
                        bankAccount.NotifyClient();
                }
            }

            return this;
        }

        public Bank SetCreditCommission(decimal creditCommission)
        {
            Conditions.CreditCommission = creditCommission;
            foreach (BankAccount bankAccount in BankAccounts)
            {
                if (bankAccount.GetType() == typeof(CreditAccount))
                {
                    if (bankAccount.BankClient.IsSubscribed)
                        bankAccount.NotifyClient();
                }
            }

            return this;
        }

        public Bank SetDoubtfulAccountLimit(decimal doubtfulAccountLimit)
        {
            Conditions.DoubtfulAccountLimit = doubtfulAccountLimit;
            foreach (BankAccount bankAccount in BankAccounts)
            {
                if (!bankAccount.IsConfirmed())
                    bankAccount.NotifyClient();
            }

            return this;
        }

        public DepositAccount CreateDepositAccount(BankClient bankClient, decimal startBalance)
        {
            var depositAccount =
                new DepositAccount
                {
                    Id = Guid.NewGuid(),
                    BankClient = bankClient,
                    CreationDate = DateTime.Now,
                    ReleaseDate = DateTime.Now + TimeSpan.FromDays(DateTime.IsLeapYear(DateTime.Now.Year) ? 366 : 355),
                    BankingConditions = Conditions,
                    InitialBalance = startBalance,
                };
            BankAccounts.Add(depositAccount);
            return depositAccount;
        }

        public DebitAccount CreateDebitAccount(BankClient bankClient, decimal initialBalance)
        {
            var debitAccount =
                new DebitAccount
                {
                    Id = Guid.NewGuid(),
                    BankClient = bankClient,
                    CreationDate = DateTime.Now,
                    BankingConditions = Conditions,
                    InitialBalance = initialBalance,
                };
            BankAccounts.Add(debitAccount);
            return debitAccount;
        }

        public CreditAccount CreateCreditAccount(BankClient bankClient, decimal initialBalance)
        {
            var creditAccount =
                new CreditAccount
                {
                    Id = Guid.NewGuid(),
                    BankClient = bankClient,
                    CreationDate = DateTime.Now,
                    BankingConditions = Conditions,
                    InitialBalance = initialBalance,
                };
            BankAccounts.Add(creditAccount);
            return creditAccount;
        }

        public void DailyRenew(DateTime currentDate)
        {
            foreach (BankAccount bankAccount in BankAccounts)
            {
                bankAccount.DailyRenew(currentDate);
            }
        }
    }
}