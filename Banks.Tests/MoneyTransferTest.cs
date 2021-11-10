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
            var debitAccount1 = new DebitAccount()
            {
                BankClient = bankClient,
                BankingConditions = conditions,
                CreationDate = DateTime.Now
            };
            const int sum = 1000;
            var debitAccount2 = new DebitAccount()
            {
                BankClient = bankClient,
                BankingConditions = conditions,
                CreationDate = DateTime.Now
            };
            debitAccount1.CreditFunds(sum);
            Assert.AreEqual(sum, debitAccount1.Balance);
            Assert.AreEqual(0, debitAccount2.Balance);
            
            
            var moneyTransfer = new MoneyTransfer()
            {
                Sender = debitAccount1,
                Receiver = debitAccount2,
                Money = sum
            };
            moneyTransfer.Commit();
            
            Assert.AreEqual(0, debitAccount1.Balance);
            Assert.AreEqual(sum, debitAccount2.Balance);
        }
    }
}