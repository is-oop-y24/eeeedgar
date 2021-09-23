﻿using System.Collections.Generic;
using Shops.Entities;
using NUnit.Framework;

namespace Shops.Tests
{
    public class BankTests
    {
        private Bank _bank;
        
        [SetUp]
        public void SetUp()
        {
            _bank = Bank.CreateInstance();
        }

        [Test]
        public void RegisterClient_ClientWasAdded()
        {
            var person = Person.CreateInstance("well", _bank);
            _bank.RegisterProfile(person);
            
            var shop = Shop.CreateInstance("oh", "no", new List<Product>());
            _bank.RegisterProfile(shop);
            
            Assert.That(_bank.HasProfile(person.Id));
            Assert.That(_bank.HasProfile(shop.Id));
        }

        [Test]
        public void MakeTransaction_BalancesChangedCorrectly()
        {
            var person = Person.CreateInstance("well", _bank);
            _bank.RegisterProfile(person);
            
            var shop = Shop.CreateInstance("oh", "no", new List<Product>());
            _bank.RegisterProfile(shop);

            _bank.MakeTransaction(person.Id, shop.Id, 100);
            Assert.AreEqual(_bank.ProfileBalance(person.Id), 0);
            Assert.AreEqual(_bank.ProfileBalance(shop.Id), 200);
        }

        [Test]
        public void TryToMakeTransactionWithoutEnoughMoney_BalancesDontChange()
        {
            var person = Person.CreateInstance("well", _bank);
            _bank.RegisterProfile(person);
            
            var shop = Shop.CreateInstance("oh", "no", new List<Product>());
            _bank.RegisterProfile(shop);

            _bank.MakeTransaction(person.Id, shop.Id, 999);
            Assert.AreEqual(_bank.ProfileBalance(person.Id), 100);
            Assert.AreEqual(_bank.ProfileBalance(shop.Id), 100);
        }
    }
}