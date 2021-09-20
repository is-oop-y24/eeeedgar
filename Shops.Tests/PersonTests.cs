using System;
using System.Collections.Generic;
using NUnit.Framework;
using Shops.Entities;

namespace Shops.Tests
{
    public class PersonTests
    {
        private Person _person;
        private Bank _bank;
        private Shop _shop;
        private List<Product> _registeredProducts;
        private Product _parrot;

        [SetUp]
        public void Setup()
        {
            _bank = Bank.CreateInstance();
            
            _registeredProducts = new List<Product>();
            _parrot = Product.CreateInstance("parrot");
            _registeredProducts.Add(_parrot);

            
            _shop = Shop.CreateInstance("okay", "i hope it's okay", _registeredProducts);
            _shop.AddPosition(_parrot);

            _person = Person.CreateInstance("well", _registeredProducts, _bank);
            
            _bank.RegisterProfile(_shop);
            _bank.RegisterProfile(_person);
        }

        [Test]
        public void MakePurchaseWithoutEnoughMoney_MoneyNotSpent()
        {
            int personBalanceBeforeAttempt = _person.Money;
            _shop.SetProductPrice(1, 9999);
            _person.Buy(_shop, _parrot, 1);
            Assert.That(_person.Money == personBalanceBeforeAttempt);
        }
        
        [Test]
        public void MakePurchaseWithEnoughMoney_MoneySpentCorrectly()
        {
            int personBalanceBeforeAttempt = _person.Money;
            const int price = 1;
            const int amount = 2;
            _shop.SetProductPrice(1, price);
            _shop.AddProducts(1, amount);
            _person.Buy(_shop, _parrot, amount);
            Assert.That(_person.Money == personBalanceBeforeAttempt - amount * price);
        }
    }
}