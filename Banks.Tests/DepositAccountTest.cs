using System;
using System.Collections.Generic;
using Banks.Model.Accounts;
using Banks.Model.Entities;
using Banks.Model.Entities.DepositStuff;
using NUnit.Framework;

namespace Banks.Tests
{
    public class DepositAccountTest
    {
        private DepositAccount _depositAccount;
        private BankingConditions _conditions;
        [SetUp]
        public void Setup()
        {
            var bankClient = new BankClient()
            {
                Name = "danya",
                Surname = "titov",
            };
            var controlBalances = new List<DepositControlBalance>
            {
                new() { Value = 100 },
                new() { Value = 200 }
            };
            var interests = new List<DepositControlInterest>
            {
                new() { Value = 3 },
                new() { Value = 5 },
                new() { Value = 7 }
            };
            var depositInterest = new DepositInterest()
            {
                ControlBalances = controlBalances,
                Interests = interests
            };
            _conditions = new BankingConditions()
            {
                DepositInterest = depositInterest,
            };
            const int sum = 1000;
            _depositAccount = new DepositAccount()
            {
                BankClient = bankClient,
                CreationDate = DateTime.Now,
                ReleaseDate = DateTime.Now,
                BankingConditions = _conditions,
                InitialBalance = sum,
                Interest = _conditions.DepositInterest.Count(sum),
            };
            _depositAccount.CreditFunds(sum);
        }

        [Test]
        public void CheckCorrectBalanceRecognising()
        {
            var controlBalances = new List<DepositControlBalance>
            {
                new() { Value = 10 },
                new() { Value = 100 },
                new() { Value = 1000 }
            };
            var interests = new List<DepositControlInterest>
            {
                new() { Value = 1 },
                new() { Value = 2 },
                new() { Value = 3 },
                new() { Value = 4 }
            };
            var depositInterest = new DepositInterest()
            {
                ControlBalances = controlBalances,
                Interests = interests,
            };
            Assert.AreEqual(depositInterest.Count(0), interests[0].Value);
            Assert.AreEqual(depositInterest.Count(11), interests[1].Value);
            Assert.AreEqual(depositInterest.Count(101), interests[2].Value);
            Assert.AreEqual(depositInterest.Count(1001), interests[3].Value);
        }

        [Test]
        public void CheckInterestAfterSomeTime()
        {
            decimal computedBalance = _depositAccount.Balance;
            int yearLength = DateTime.IsLeapYear(_depositAccount.CreationDate.Year) ? 366 : 365;
            for (int d = 1; d <= yearLength; d++)
            {
                _depositAccount.DailyRenew(_depositAccount.CreationDate + TimeSpan.FromDays(d));
            }

            for (int m = 1; m <= 12; m++)
            {
                computedBalance *= 1 + _conditions.DepositInterest.Count(computedBalance) / 100 / 12;
            }
            Assert.AreEqual(decimal.Round(computedBalance, 2), decimal.Round(_depositAccount.Balance, 2));
        }
    }
}