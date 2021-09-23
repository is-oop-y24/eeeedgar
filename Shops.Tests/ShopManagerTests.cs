using System.Collections;
using System.Linq;
using NUnit.Framework;
using Shops.Entities;
using Shops.Services;

namespace Shops.Tests
{
    public class ShopManagerTests
    {
        private ShopManager _shopManager;

        [SetUp]
        public void Setup()
        {
            _shopManager = ShopManager.CreateInstance();
        }

        [Test]
        public void AddShop_ShopManagerContainsShop()
        {
            Shop shop = _shopManager.RegisterShop("Spar", "Furshtatskaya");
            Assert.Contains(shop, _shopManager.Shops.ToList());
        }
        
        [Test]
        public void AddPerson_ShopManagerContainsPerson()
        {
            Person person = _shopManager.RegisterPerson("edgar");
            Assert.Contains(person, _shopManager.Persons.ToList());
        }
        
        [Test]
        public void AddProduct_ShopManagerContainsProduct()
        {
            Product product = _shopManager.RegisterProduct("corn");
            Assert.Contains(product, _shopManager.Products.ToList());
        }
    }
}