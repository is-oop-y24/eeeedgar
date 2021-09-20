using System.Collections.Generic;
using Shops.Entities;
using NUnit.Framework;

namespace Shops.Tests
{
    public class BankTests
    {
        private Bank _bank;
        private List<Product> _registeredProducts;

        [SetUp]
        public void SetUp()
        {
            _bank = Bank.CreateInstance();
            _registeredProducts = new List<Product>();
        }

        [Test]
        public void RegisterProfile_ProfileWasAdd()
        {
            var person = Person.CreateInstance("well", _registeredProducts, _bank);
            _bank.RegisterProfile(person);
            Assert.That(_bank.HasProfile(person.Id));
        }

        [Test]
        public void MakeTransaction_BalancesChangedCorrectly()
        {
            var person = Person.CreateInstance("well", _registeredProducts, _bank);
            _bank.RegisterProfile(person);
            
            var shop = Shop.CreateInstance("oh", "no", _registeredProducts);
            _bank.RegisterProfile(shop);

            _bank.MakeTransaction(person.Id, shop.Id, 100);
            Assert.That(_bank.ProfileBalance(person.Id) == 0);
            Assert.That(_bank.ProfileBalance(shop.Id) == 200);
        }

        [Test]
        public void TryToMakeTransactionWithoutEnoughMoney_BalancesDontChange()
        {
            var person = Person.CreateInstance("well", _registeredProducts, _bank);
            _bank.RegisterProfile(person);
            
            var shop = Shop.CreateInstance("oh", "no", _registeredProducts);
            _bank.RegisterProfile(shop);

            _bank.MakeTransaction(person.Id, shop.Id, 999);
            Assert.That(_bank.ProfileBalance(person.Id) == 100);
            Assert.That(_bank.ProfileBalance(shop.Id) == 100);
        }
    }
}