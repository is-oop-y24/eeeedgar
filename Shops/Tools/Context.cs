using Shops.Entities;
using Shops.Services;

namespace Shops.Tools
{
    public class Context
    {
        public Context(Customer customer = null, Shop shop = null, ShopManager shopManager = null)
        {
            Customer = customer;
            Shop = shop;
            ShopManager = shopManager;
        }

        public string Choice { get; }
        public ShopManager ShopManager { get; }
        public Customer Customer { get; }
        public Shop Shop { get; }
    }
}