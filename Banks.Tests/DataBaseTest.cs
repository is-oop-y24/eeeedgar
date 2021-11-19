using System;
using System.Collections.Generic;
using System.Linq;
using Banks.Model.Accounts;
using Banks.Model.Entities;
using Banks.Model.Entities.DepositStuff;
using Banks.Model.Transactions;
using Banks.Repository;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Banks.Tests
{
    public class DataBaseTest
    {
        [SetUp]
        public void Setup()
        {
        }
        [Test]
        public void LoadChangesToDataBase_CheckAmount()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();

            DbContextOptions<ApplicationContext> options = optionsBuilder
                .UseInMemoryDatabase("banksDB")
                .Options;
            var db = new ApplicationContext(options);
            var centralBank = new CentralBank();

            centralBank.RegisterBank(
                Bank.CreateInstance(
                    "sber",
                    new DepositInterest(),
                    3, 15000,
                    200,
                    5000));
            centralBank.RegisterClient(
                BankClient.CreateInstance("ve", "ka")
                    .SetAddress("address")
                    .SetPassportData("228 1337 666")
                    .Subscribe());
            Console.WriteLine(centralBank.Clients.Count);
            centralBank.Banks.First().CreateDepositAccount(centralBank.Clients.First(), 0);
            var accountReplenishment = new AccountReplenishment
            {
                Id = Guid.NewGuid(),
                Money = 10,
                Receiver = centralBank.Banks.First().BankAccounts.First(),
            };
            centralBank.MakeTransaction(accountReplenishment);

            db.CentralBanks.Add(centralBank);
            db.SaveChanges();

            var cbs = db.CentralBanks.ToList();
            Assert.AreEqual(1, cbs.Count);

            CentralBank cb = cbs.Last();
            Assert.AreEqual(1, cb.Clients.Count);
            
            Assert.AreEqual(1, cb.Banks.Count);

            List<BankAccount> bankAccounts = cb.Banks.First().BankAccounts;
            Assert.AreEqual(1, bankAccounts.Count);
        }
    }
}