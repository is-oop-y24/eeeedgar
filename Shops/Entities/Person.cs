using System.Collections.Generic;
using System.Dynamic;
using Shops.Services;

namespace Shops.Entities
{
    public class Person : BankClient
    {
        private List<Purchase> _wishList;

        private Person(string name, ShopManager shopManager)
        {
            Name = name;
            _wishList = new List<Purchase>();
            ShopManager = shopManager;
        }

        public string Name { get; }
        public IReadOnlyList<Purchase> WishList => _wishList;
        public ShopManager ShopManager { get; }

        public static Person CreateInstance(string name, ShopManager shopManager)
        {
            return new Person(name, shopManager);
        }

        public void Buy(Shop shop)
        {
            shop.MakeDeal(this);
        }

        public void AddItemToWishList(Product product, int amount = 1)
        {
            _wishList.Add(Purchase.CreateInstance(product, amount));
        }

        public void AddItemToWishList(int id, int amount)
        {
            Product product = ShopManager.GetProduct(id);
            AddItemToWishList(product, amount);
        }
    }
}