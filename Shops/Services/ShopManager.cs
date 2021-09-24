using System;
using System.Collections.Generic;
using Shops.Entities;

namespace Shops.Services
{
    public class ShopManager : IShopManager, IObserver
    {
        private List<Shop> _shops;
        private List<Customer> _customers;
        private List<Product> _products;
        private Bank _bank;

        private ShopManager()
        {
            _shops = new List<Shop>();
            _customers = new List<Customer>();
            _products = new List<Product>();
            _bank = Bank.CreateInstance();
        }

        public IReadOnlyList<Shop> Shops => _shops;
        public IReadOnlyList<Customer> Customers => _customers;
        public IReadOnlyList<Product> Products => _products;

        public static ShopManager CreateInstance()
        {
            return new ShopManager();
        }

        public Product CreateProduct(string productName)
        {
            Product product = _products.Find(pr => pr.Name.Equals(productName));
            if (product != null)
                return product;

            product = Product.CreateInstance(productName);
            _products.Add(product);
            return product;
        }

        public Shop CreateShop(string shopName, string shopAddress)
        {
            var shop = Shop.CreateInstance(shopName, shopAddress, _products);
            _shops.Add(shop);

            _bank.RegisterProfile(shop);
            return shop;
        }

        public Customer CreateCustomer(string name)
        {
            var person = Customer.CreateInstance(name, _bank);
            _customers.Add(person);

            _bank.RegisterProfile(person);
            person.Attach(this);
            return person;
        }

        public Product GetProduct(int id)
        {
            Product product = _products.Find(pr => pr.Id == id);
            if (product == null)
                throw new Exception("wrong product id");
            return product;
        }

        public Shop GetShop(int id)
        {
            Shop shop = _shops.Find(sh => sh.Id == id);
            if (shop == null)
                throw new Exception("wrong shop id");
            return shop;
        }

        public Customer GetPerson(int id)
        {
            Customer customer = _customers.Find(per => per.Id == id);
            if (customer == null)
                throw new Exception("wrong person id");
            return customer;
        }

        public void Update(ISubject subject)
        {
            MakeDeal((Customer)subject);
        }

        private bool MakeDeal(Customer customer)
        {
            Shop shop = GetShop(customer.SelectedShopId);
            int productId = customer.SelectedProductId;
            int productAmountToBuy = customer.SelectedProductAmount;
            if (!shop.CanSell(productId, productAmountToBuy))
            {
                return false;
            }

            if (!_bank.MakeTransaction(customer.Id, shop.Id, shop.PurchaseCost(productId, productAmountToBuy)))
            {
                return false;
            }

            shop.Sell(productId, productAmountToBuy);
            return true;
        }
    }
}