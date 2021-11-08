using System;
using System.Collections.Generic;
using Banks.Model.Accounts;
using Banks.Model.Entities;
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
            var controlBalances = new List<decimal>()
            {
                100,
                200,
            };
            var interests = new List<decimal>()
            {
                3,
                5,
                7,
            };
            var depositInterest = new DepositInterest(controlBalances, interests);
            _conditions = new BankingConditions()
            {
                DepositInterest = depositInterest,
            };
            _depositAccount = new DepositAccount(bankClient, DateTime.Now, DateTime.Now, _conditions);
            const int sum = 1000;
            _depositAccount.CreditFunds(sum);
        }

        [Test]
        public void CheckCorrectBalanceRecognising()
        {
            var controlBalances = new List<decimal>
            {
                10,
                100,
                1000
            };
            var interests = new List<decimal>
            {
                1,
                2,
                3,
                4
            };
            var depositInterest = new DepositInterest(controlBalances, interests);
            Assert.AreEqual(depositInterest.Interest(0), interests[0]);
            Assert.AreEqual(depositInterest.Interest(11), interests[1]);
            Assert.AreEqual(depositInterest.Interest(101), interests[2]);
            Assert.AreEqual(depositInterest.Interest(1001), interests[3]);
        }

        [Test]
        public void CheckInterestAfterSomeTime()
        {
            decimal computedBalance = _depositAccount.Balance();
            int yearLength = DateTime.IsLeapYear(_depositAccount.CreationDate.Year) ? 366 : 365;
            for (int d = 1; d <= yearLength; d++)
            {
                _depositAccount.DailyRenew(_depositAccount.CreationDate + TimeSpan.FromDays(d));
            }

            for (int m = 1; m <= 12; m++)
            {
                computedBalance *= 1 + _conditions.DepositInterest.Interest(computedBalance) / 100 / 12;
            }
            Assert.AreEqual(decimal.Round(computedBalance, 2), decimal.Round(_depositAccount.Balance(), 2));
        }
    }
}