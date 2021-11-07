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
            _debitAccount = new DebitAccount(bankClient, 1);
            const int sum = 1000;
            _debitAccount.ReceiveMoney(sum);
        }

        [Test]
        public void DebitAccounts()
        {
            decimal oldBalance = _debitAccount.Balance();
            const int yearLength = 365;
            _debitAccount.ScheduleRenew(yearLength);
            Assert.AreEqual(oldBalance * (1 + _debitAccount.Interest / 100), _debitAccount.Balance());
        }
    }
}