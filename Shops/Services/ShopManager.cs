using System;
using System.Collections.Generic;
using Shops.Entities;
using Shops.Tools;

namespace Shops.Services
{
    public class ShopManager : IShopManager
    {
        private List<Shop> _shops;
        private List<Customer> _customers;
        private List<Product> _products;
        private Bank _bank;

        public ShopManager()
        {
            _shops = new List<Shop>();
            _customers = new List<Customer>();
            _products = new List<Product>();
            _bank = new Bank();
        }

        public IReadOnlyList<Shop> Shops => _shops;
        public IReadOnlyList<Customer> Customers => _customers;
        public IReadOnlyList<Product> Products => _products;

        public Product RegisterProduct(string productName)
        {
            Product product = FindProduct(productName);
            if (product != null)
                throw new Exception("product with this name is already registered");

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

        public Customer RegisterCustomer(string name, int balance)
        {
            var customer = Customer.CreateInstance(name);
            _customers.Add(customer);

            _bank.RegisterProfile(customer, balance);
            return customer;
        }

        public Product GetProduct(int productId)
        {
            return _products.Find(pr => pr.Id == productId) ?? throw new ShopException("wrong product id");
        }

        public Shop GetShop(int shopId)
        {
            return _shops.Find(sh => sh.Id == shopId) ?? throw new ShopException("wrong shop id");
        }

        public Customer GetCustomer(int customerId)
        {
            return _customers.Find(per => per.Id == customerId) ?? throw new ShopException("wrong person id");
        }

        public void MakeDeal(Customer customer, Shop shop, int productId, int productAmount)
        {
            if (!shop.CanSell(productId, productAmount))
                throw new ShopException("shop can't sell it");
            int purchasePrice = shop.PurchasePrice(productId, productAmount);
            _bank.MakeTransaction(customer.Id, shop.Id, purchasePrice);
            shop.Sell(productId, productAmount);
        }

        public int Balance(int clientId)
        {
            return _bank.ProfileBalance(clientId);
        }

        private Product FindProduct(string productName)
        {
            return _products.Find(product => product.Name.Equals(productName));
        }
    }
}