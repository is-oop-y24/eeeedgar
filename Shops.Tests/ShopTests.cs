using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Shops.Entities;

namespace Shops.Tests
{
    public class ShopTests
    {
        private Shop _shop;
        private List<Product> _registeredProducts;
        private Product _product;

        [SetUp]
        public void SetUp()
        {
            _registeredProducts = new List<Product>();
            _product = Product.CreateInstance("corn");
            _registeredProducts.Add(_product);
            _shop = Shop.CreateInstance("spar", "fursh", _registeredProducts);
        }
        [Test]
        public void AddPositionToShop_ShopHasPosition()
        {
            _shop.AddPosition(_product);
            Assert.That(_shop.HasPosition(_product));
        }

        [Test]
        public void AddProducts_ProductAmountChangesCorrectly()
        {
            const int amount = 10;
            _shop.AddPosition(_product);
            _shop.AddProducts(_product, amount);
            Position position = _shop.GetPosition(_product);
            Assert.That(position.Amount == amount);
        }

        [Test]
        public void SellProduct_ProductAmountChangedCorrectly()
        {
            const int addedAmount = 10;
            const int soldAmount = 6;
            _shop.AddPosition(_product);
            _shop.AddProducts(_product, addedAmount);
            _shop.Sell(_product, soldAmount);
            Position position = _shop.GetPosition(_product);
            Assert.That(position.Amount == addedAmount - soldAmount);
        }

        [Test]
        public void SetPrice_PriceWasSetCorrectly()
        {
            const int price = 30;
            _shop.AddPosition(_product);
            _shop.SetProductPrice(_product, price);
            Position position = _shop.GetPosition(_product);
            Assert.That(position.Cost == price);
        }
    }
}