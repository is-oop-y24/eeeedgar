using System;
using System.Collections.Generic;
using System.Linq;
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

        public static Bank CreateInstance(string name, DepositInterest depositInterest, decimal debitInterest, decimal creditLimit, decimal creditCommission, decimal doubtfulAccountLimit)
        {
            var conditions = new BankingConditions()
            {
                DepositInterest = depositInterest,
                DebitInterest = debitInterest,
                CreditCommission = creditCommission,
                CreditLimit = creditLimit,
                DoubtfulAccountLimit = doubtfulAccountLimit,
            };
            return new Bank
            {
                Id = Guid.NewGuid(),
                Name = name,
                Conditions = conditions,
            };
        }

        public void SetDepositInterest(DepositInterest depositInterest)
        {
            Conditions.DepositInterest = depositInterest;
            foreach (DepositAccount depositAccount in BankAccounts.OfType<DepositAccount>())
            {
                if (depositAccount.BankClient.IsSubscribed)
                        depositAccount.NotifyClient();
            }
        }

        public void SetDebitInterest(decimal debitInterest)
        {
            Conditions.DebitInterest = debitInterest;
            foreach (DebitAccount debitAccount in BankAccounts.OfType<DebitAccount>())
            {
                if (debitAccount.BankClient.IsSubscribed)
                    debitAccount.NotifyClient();
            }
        }

        public void SetCreditLimit(decimal creditLimit)
        {
            Conditions.CreditLimit = creditLimit;
            foreach (CreditAccount creditAccount in BankAccounts.OfType<CreditAccount>())
            {
                if (creditAccount.BankClient.IsSubscribed)
                    creditAccount.NotifyClient();
            }
        }

        public void SetCreditCommission(decimal creditCommission)
        {
            Conditions.CreditCommission = creditCommission;
            foreach (CreditAccount creditAccount in BankAccounts.OfType<CreditAccount>())
            {
                if (creditAccount.BankClient.IsSubscribed)
                    creditAccount.NotifyClient();
            }
        }

        public void SetDoubtfulAccountLimit(decimal doubtfulAccountLimit)
        {
            Conditions.DoubtfulAccountLimit = doubtfulAccountLimit;
            foreach (BankAccount bankAccount in BankAccounts.Where(bankAccount => !bankAccount.IsConfirmed()))
            {
                bankAccount.NotifyClient();
            }
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