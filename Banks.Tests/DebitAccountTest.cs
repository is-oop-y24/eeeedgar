using System;
using Banks.Model.Accounts;
using Banks.Model.Entities;
using NUnit.Framework;

namespace Banks.Tests
{
    public class DebitAccountTest
    {
        private DebitAccount _debitAccount = null;
        [SetUp]
        public void Setup()
        {
            var bankClient = new BankClient()
            {
                Name = "danya",
                Surname = "titov",
            };
            var conditions = new BankingConditions()
            {
                DebitInterest = 1,
            };
            _debitAccount = new DebitAccount(bankClient, conditions, DateTime.Now);
            const int sum = 1000;
            _debitAccount.CreditFunds(sum);
        }

        [Test]
        public void DebitAccounts()
        {
            decimal computedBalance = _debitAccount.Balance();
            int yearLength = DateTime.IsLeapYear(_debitAccount.CreationDate.Year) ? 366 : 365;
            for (int d = 1; d <= yearLength; d++)
            {
                _debitAccount.DailyRenew(_debitAccount.CreationDate + TimeSpan.FromDays(d));
            }

            for (int m = 1; m <= 12; m++)
            {
                computedBalance *= 1 + ((decimal) 0.01 / 12);
            }
            Assert.AreEqual(decimal.Round(computedBalance, 4), decimal.Round(_debitAccount.Balance(), 4));
        }
    }
}