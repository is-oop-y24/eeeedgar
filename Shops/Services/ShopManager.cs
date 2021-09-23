using System;
using System.Collections.Generic;
using Shops.Entities;

namespace Shops.Services
{
    public class ShopManager : IShopManager, IObserver
    {
        private List<Shop> _shops;
        private List<Person> _persons;
        private List<Product> _products;
        private Bank _bank;

        private ShopManager()
        {
            _shops = new List<Shop>();
            _persons = new List<Person>();
            _products = new List<Product>();
            _bank = Bank.CreateInstance();
        }

        public IReadOnlyList<Shop> Shops => _shops;
        public IReadOnlyList<Person> Persons => _persons;
        public IReadOnlyList<Product> Products => _products;

        public static ShopManager CreateInstance()
        {
            return new ShopManager();
        }

        public Product RegisterProduct(string productName)
        {
            Product product = _products.Find(pr => pr.Name.Equals(productName));
            if (product != null)
                return product;

            product = Product.CreateInstance(productName);
            _products.Add(product);
            return product;
        }

        public Shop RegisterShop(string shopName, string shopAddress)
        {
            var shop = Shop.CreateInstance(shopName, shopAddress, _products);
            _shops.Add(shop);

            _bank.RegisterProfile(shop);
            return shop;
        }

        public Person RegisterPerson(string name)
        {
            var person = Person.CreateInstance(name, _bank);
            _persons.Add(person);

            _bank.RegisterProfile(person);
            person.Attach(this);
            return person;
        }

        public Product GetProduct(int id)
        {
            Product product = _products.Find(pr => pr.Id == id);
            if (product != null)
                return product;
            throw new Exception("wrong product id");
        }

        public Shop GetShop(int id)
        {
            Shop shop = _shops.Find(sh => sh.Id == id);
            if (shop != null)
                return shop;
            throw new Exception("wrong shop id");
        }

        public Person GetPerson(int id)
        {
            Person person = _persons.Find(per => per.Id == id);
            if (person != null)
                return person;
            throw new Exception("wrong person id");
        }

        public void Update(ISubject subject)
        {
            MakeDeal((Person)subject);
        }

        private bool MakeDeal(Person person)
        {
            Shop shop = GetShop(person.SelectedShopId);
            int productId = person.SelectedProductId;
            int productAmountToBuy = person.SelectedProductAmount;
            if (!shop.CanSell(productId, productAmountToBuy))
            {
                return false;
            }

            if (!_bank.MakeTransaction(person.Id, shop.Id, shop.PurchaseCost(productId, productAmountToBuy)))
            {
                return false;
            }

            shop.Sell(productId, productAmountToBuy);
            return true;
        }
    }
}