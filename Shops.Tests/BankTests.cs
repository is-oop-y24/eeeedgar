using System.Collections.Generic;
using Shops.Entities;
using NUnit.Framework;
using Shops.Tools;

namespace Shops.Tests
{
    public class BankTests
    {
        private Bank _bank;
        
        [SetUp]
        public void SetUp()
        {
            _bank = new Bank();
        }

        [Test]
        public void MakeTransaction_BalancesChangedCorrectly()
        {
            var person = Customer.CreateInstance("well");
            _bank.RegisterProfile(person);
            
            var shop = Shop.CreateInstance("oh", "no", new List<Product>());
            _bank.RegisterProfile(shop);

            _bank.MakeTransaction(person.Id, shop.Id, 100);
            Assert.AreEqual(_bank.ProfileBalance(person.Id), 0);
            Assert.AreEqual(_bank.ProfileBalance(shop.Id), 200);
        }

        [Test]
        public void TryToMakeTransactionWithoutEnoughMoney_ThrowException()
        {
            
            var person = Customer.CreateInstance("well");
            _bank.RegisterProfile(person);
            
            var shop = Shop.CreateInstance("oh", "no", new List<Product>());
            _bank.RegisterProfile(shop);

            Assert.Catch<ShopException>(() =>
            {
                _bank.MakeTransaction(person.Id, shop.Id, 999);
            });
        }
    }
}