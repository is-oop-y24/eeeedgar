using Banks.Accounts;
using Banks.Entities;
using Banks.Transactions;
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
            };
            var debitAccount1 = new DebitAccount(bankClient, 1);
            const int sum = 1000;
            var debitAccount2 = new DebitAccount(bankClient, 1);
            debitAccount1.ReceiveMoney(sum);
            Assert.AreEqual(debitAccount1.Balance(), sum);
            Assert.AreEqual(debitAccount2.Balance(), 0);
            
            
            var moneyTransfer = new MoneyTransfer(debitAccount1, debitAccount2, sum);
            moneyTransfer.Make();
            
            Assert.AreEqual(debitAccount1.Balance(), 0);
            Assert.AreEqual(debitAccount2.Balance(), sum);
        }
    }
}