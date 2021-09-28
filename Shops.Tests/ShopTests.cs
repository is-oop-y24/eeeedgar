using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Shops.Entities;
using Shops.Services;

namespace Shops.Tests
{
    public class ShopTests
    {
        private ShopManager _shopManager;
        private Shop _shop;
        private Product _product;

        [SetUp]
        public void SetUp()
        {
            _shopManager = new ShopManager();
            _product = _shopManager.RegisterProduct("corn");
            _shop = _shopManager.RegisterShop("spar", "fursh");
        }
        [Test]
        public void AddPositionToShop_ShopHasPosition()
        {
            _shop.AddPosition(_product.Id);
            Assert.IsTrue(_shop.Stock.ToList().Find(position => position.Product.Name == _product.Name) != null);
        }

        [Test]
        public void AddProducts_ProductAmountChangesCorrectly()
        {
            const int amount = 10;
            _shop.AddPosition(_product.Id);
            _shop.AddProducts(_product.Id, amount);
            Position position = _shop.Stock.ToList().Find(pos => pos.Product.Id == _product.Id);
            Assert.That(position is {Amount: amount});
        }

        [Test]
        public void SellProduct_ProductAmountChangedCorrectly()
        {
            const int addedAmount = 10;
            const int soldAmount = 6;
            _shop.AddPosition(_product.Id);
            _shop.AddProducts(_product.Id, addedAmount);
            _shop.Sell(_product.Id, soldAmount);
            Position position = _shop.Stock.ToList().Find(pos => pos.Product.Id == _product.Id);
            Assert.That(position is {Amount: addedAmount - soldAmount});
        }

        [Test]
        public void SetPrice_PriceWasSetCorrectly()
        {
            const int price = 30;
            _shop.AddPosition(_product.Id);
            _shop.SetProductPrice(_product.Id, price);
            Position position = _shop.Stock.ToList().Find(pos => pos.Product.Id == _product.Id);
            Assert.That(position is {Cost: price});
        }
    }
}