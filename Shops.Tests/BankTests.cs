using System.Collections.Generic;
using Shops.Entities;
using NUnit.Framework;
using Shops.Tools;

namespace Shops.Tests
{
    public class BankTests
    {
        private PaymentSystem _paymentSystem;
        
        [SetUp]
        public void SetUp()
        {
            _paymentSystem = new PaymentSystem();
        }

        [Test]
        public void MakeTransaction_BalancesChangedCorrectly()
        {
            var person = Customer.CreateInstance("well");
            PaymentSystem.RegisterProfile(person);
            
            var shop = new Shop("oh", "no");
            PaymentSystem.RegisterProfile(shop);

            PaymentSystem.MakeTransaction(person.Id, shop.Id, 100);
            Assert.AreEqual(PaymentSystem.ProfileBalance(person.Id), 0);
            Assert.AreEqual(PaymentSystem.ProfileBalance(shop.Id), 200);
        }

        [Test]
        public void TryToMakeTransactionWithoutEnoughMoney_ThrowException()
        {
            var person = Customer.CreateInstance("well");
            PaymentSystem.RegisterProfile(person);
            
            var shop = new Shop("oh", "no");
            PaymentSystem.RegisterProfile(shop);

            Assert.Catch<ShopException>(() =>
            {
                PaymentSystem.MakeTransaction(person.Id, shop.Id, 999);
            });
        }
    }
}