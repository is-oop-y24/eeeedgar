using System;
using Banks.Model.Accounts;
using Banks.Model.Entities;
using Banks.Model.Transactions;
using NUnit.Framework;

namespace Banks.Tests
{
    public class MoneyTransferTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void DebitAccounts()
        {
            var bankClient = new BankClient()
            {
                Name = "danya",
                Surname = "titov",
                Address = "kyiv",
                PassportData = "228 1337",
            };
            var conditions = new BankingConditions();
            var debitAccount1 = new DebitAccount(bankClient, conditions, DateTime.Now);
            const int sum = 1000;
            var debitAccount2 = new DebitAccount(bankClient, conditions, DateTime.Now);
            debitAccount1.CreditFunds(sum);
            Assert.AreEqual(debitAccount1.Balance(), sum);
            Assert.AreEqual(debitAccount2.Balance(), 0);
            
            
            var moneyTransfer = new MoneyTransfer(debitAccount1, debitAccount2, sum);
            moneyTransfer.Commit();
            
            Assert.AreEqual(0, debitAccount1.Balance());
            Assert.AreEqual(sum, debitAccount2.Balance());
        }
    }
}