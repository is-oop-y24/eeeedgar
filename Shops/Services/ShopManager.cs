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

        public ShopManager()
        {
            _shops = new List<Shop>();
            _customers = new List<Customer>();
        }

        public IReadOnlyList<Shop> Shops => _shops;
        public IReadOnlyList<Customer> Customers => _customers;

        public Shop RegisterShop(string shopName, string shopAddress)
        {
            var shop = new Shop(shopName, shopAddress);
            _shops.Add(shop);

            PaymentSystem.RegisterProfile(shop);
            return shop;
        }

        public Customer RegisterCustomer(string name, int balance)
        {
            var customer = new Customer(name);
            _customers.Add(customer);

            PaymentSystem.RegisterProfile(customer, balance);
            return customer;
        }

        public Product GetProduct(int productId)
        {
            return GlobalProductBase.GetProduct(productId);
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
            PaymentSystem.MakeTransaction(customer.Id, shop.Id, purchasePrice);
            shop.Sell(productId, productAmount);
        }

        public int Balance(int clientId)
        {
            return PaymentSystem.ProfileBalance(clientId);
        }
    }
}