using System.Collections.Generic;
using Shops.Entities;
using NUnit.Framework;
using Shops.Tools;

namespace Shops.Tests
{
    public class BankTests
    { 
        [Test]
        public void MakeTransaction_BalancesChangedCorrectly()
        {
            const int transactionValue = 100;
            var person = new Customer("well");
            PaymentSystem.RegisterProfile(person, transactionValue);
            
            var shop = new Shop("oh", "no");
            PaymentSystem.RegisterProfile(shop);

            PaymentSystem.MakeTransaction(person.Id, shop.Id, transactionValue);
            Assert.AreEqual(PaymentSystem.ProfileBalance(person.Id), 0);
            Assert.AreEqual(PaymentSystem.ProfileBalance(shop.Id), transactionValue);
        }

        [Test]
        public void TryToMakeTransactionWithoutEnoughMoney_ThrowException()
        {
            var person = new Customer("well");
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