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

        [SetUp]
        public void SetUp()
        {
            _shopManager = new ShopManager();
            _shop = _shopManager.RegisterShop("spar", "fursh");
        }

        [Test]
        public void AddProducts_ProductAmountChangesCorrectly()
        {
            const int amount = 10;
            Product product = GlobalProductBase.RegisterProduct("porigge");
            _shop.AddPosition(product.Id);
            _shop.AddProducts(product.Id, amount);
            StockPosition stockPosition = _shop.Stock.ToList().Find(pos => pos.Product.Id == product.Id);
            Assert.That(stockPosition is {Amount: amount});
        }

        [Test]
        public void SellProduct_ProductAmountChangedCorrectly()
        {
            const int addedAmount = 10;
            const int soldAmount = 6;
            Product product = GlobalProductBase.RegisterProduct("lalalalala");
            _shop.AddPosition(product.Id);
            _shop.AddProducts(product.Id, addedAmount);
            _shop.Sell(new Purchase(product, soldAmount));
            StockPosition stockPosition = _shop.Stock.ToList().Find(pos => pos.Product.Id == product.Id);
            Assert.That(stockPosition is {Amount: addedAmount - soldAmount});
        }

        [Test]
        public void SetPrice_PriceWasSetCorrectly()
        {
            const int addedAmount = 10;
            const int price = 30;
            Product product = GlobalProductBase.RegisterProduct("lalalalalalo");
            _shop.AddPosition(product.Id);
            _shop.AddProducts(product.Id, addedAmount);
            _shop.SetProductPrice(product.Id, price);
            StockPosition stockPosition = _shop.Stock.ToList().Find(pos => pos.Product.Id == product.Id);
            Assert.That(stockPosition is {Cost: price});
        }
    }
}