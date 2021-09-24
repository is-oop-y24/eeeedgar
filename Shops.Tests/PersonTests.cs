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
        private Customer _customer;
        private Shop _shop;
        private Product _product;

        [SetUp]
        public void Setup()
        {
            _shopManager = ShopManager.CreateInstance();
            _product = _shopManager.CreateProduct("parrot");
            _shop = _shopManager.CreateShop("dixy", "kronva");
            _customer = _shopManager.CreateCustomer("well");

            const int productAmount = 100;
            _shop.AddPosition(_product.Id);
            _shop.AddProducts(_product.Id, productAmount);
        }

        [Test]
        public void MakePurchaseWithoutEnoughMoney_MoneyNotSpent()
        {
            const int bigPrice = 99999;
            int personBalanceBeforeAttempt = _customer.Money;
            _shop.SetProductPrice(_product.Id, bigPrice);
            _customer.MakePurchase(_shop.Id, _product.Id, 1);
            Assert.AreEqual(_customer.Money, personBalanceBeforeAttempt);
        }
        
        [Test]
        public void MakePurchaseWithEnoughMoney_MoneySpentCorrectly()
        {
            int personBalanceBeforeAttempt = _customer.Money;
            const int price = 1;
            const int amountToBuy = 2;
            _shop.SetProductPrice(_product.Id, price);
            _customer.MakePurchase(_shop.Id, _product.Id, amountToBuy);
            Assert.AreEqual(personBalanceBeforeAttempt - amountToBuy * price, _customer.Money);
        }
    }
}