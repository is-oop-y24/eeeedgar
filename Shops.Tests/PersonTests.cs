using System;
using System.Collections.Generic;
using NUnit.Framework;
using Shops.Entities;
using Shops.Services;

namespace Shops.Tests
{
    public class PersonTests
    {
        private ShopManager _shopManager;
        private Person _person;
        private Shop _shop;
        private Product _product;

        [SetUp]
        public void Setup()
        {
            _shopManager = ShopManager.CreateInstance();
            _product = _shopManager.RegisterProduct("parrot");
            _shop = _shopManager.RegisterShop("dixy", "kronva");
            _person = _shopManager.RegisterPerson("well");

            const int productAmount = 100;
            _shop.AddPosition(_product.Id);
            _shop.AddProducts(_product.Id, productAmount);
        }

        [Test]
        public void MakePurchaseWithoutEnoughMoney_MoneyNotSpent()
        {
            const int bigPrice = 99999;
            int personBalanceBeforeAttempt = _person.Money;
            _shop.SetProductPrice(_product.Id, bigPrice);
            _person.MakePurchase(_shop.Id, _product.Id, 1);
            Assert.AreEqual(_person.Money, personBalanceBeforeAttempt);
        }
        
        [Test]
        public void MakePurchaseWithEnoughMoney_MoneySpentCorrectly()
        {
            int personBalanceBeforeAttempt = _person.Money;
            const int price = 1;
            const int amountToBuy = 2;
            _shop.SetProductPrice(_product.Id, price);
            _person.MakePurchase(_shop.Id, _product.Id, amountToBuy);
            Assert.AreEqual(personBalanceBeforeAttempt - amountToBuy * price, _person.Money);
        }
    }
}