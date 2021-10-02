using System.Linq;
using NUnit.Framework;
using Shops.Entities;
using Shops.Services;
using Shops.Tools;

namespace Shops.Tests
{
    public class ShopManagerTests
    {
        private ShopManager _shopManager;

        [SetUp]
        public void Setup()
        {
            _shopManager = new ShopManager();
        }

        [Test]
        public void AddShop_ShopManagerContainsShop()
        {
            Shop shop = _shopManager.RegisterShop("Spar", "Furshtatskaya");
            Assert.Contains(shop, _shopManager.Shops.ToList());
        }
        
        [Test]
        public void AddCustomer_ShopManagerContainsCustomer()
        {
            const int startBalance = 100;
            Customer customer = _shopManager.RegisterCustomer("edgar", startBalance);
            Assert.Contains(customer, _shopManager.Customers.ToList());
        }
        
        [Test]
        public void AddProduct_ShopManagerContainsProduct()
        {
            Product product = GlobalProductBase.RegisterProduct("corn");
            Assert.Contains(product, GlobalProductBase.GetInstance().ToList());
        }

        [Test]
        public void MakePurchaseWithoutEnoughMoney_ThrowException()
        {
            const int bigPrice = 99999;
            const int productAmount = 100;
            const int startBalance = 100;
            Customer customer = _shopManager.RegisterCustomer("nikolai", startBalance);
            Shop shop = _shopManager.RegisterShop("pyatyorochka", "smolenskaya");
            Product product = GlobalProductBase.RegisterProduct("bepiss");
            shop.AddPosition(product.Id);
            shop.SetProductPrice(product.Id, bigPrice);
            shop.AddProducts(product.Id, productAmount);
            Assert.Catch<ShopException>(() =>
            {
                _shopManager.MakeDeal(customer, shop, new Purchase(product, productAmount));
            });
        }

        [Test]
        public void MakePurchaseWithEnoughMoney_MoneySpentCorrectly()
        {
            const int lowPrice = 1;
            const int productAmount = 100;
            const int startBalance = 100;
            Customer customer = _shopManager.RegisterCustomer("nikolai", startBalance);
            Shop shop = _shopManager.RegisterShop("pyatyorochka", "smolenskaya");
            Product product = GlobalProductBase.RegisterProduct("bepis");
            shop.AddPosition(product.Id);
            shop.SetProductPrice(product.Id, lowPrice);
            shop.AddProducts(product.Id, productAmount);
            _shopManager.MakeDeal(customer, shop, new Purchase(product, productAmount));
            Assert.AreEqual(_shopManager.Balance(customer.Id), startBalance - lowPrice * productAmount);
        }
    }
}