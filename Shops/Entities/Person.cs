using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Shops.Services;

namespace Shops.Entities
{
    public class Person : BankClient
    {
        private List<Purchase> _wishList;

        private Person(string name, IReadOnlyList<Product> permittedProducts)
        {
            Name = name;
            _wishList = new List<Purchase>();
            PermittedProducts = permittedProducts;
        }

        public string Name { get; }
        public IReadOnlyList<Purchase> WishList => _wishList;
        public IReadOnlyList<Product> PermittedProducts { get; }

        public static Person CreateInstance(string name, IReadOnlyList<Product> permittedProducts)
        {
            return new Person(name, permittedProducts);
        }

        /*
        public void Buy(Shop shop)
        {
            shop.MakeDeal(this);
        }
        */

        public void AddItemToWishList(Product product, int amount = 1)
        {
            _wishList.Add(Purchase.CreateInstance(product, amount));
        }

        public void AddItemToWishList(int id, int amount)
        {
            Product product = PermittedProducts.FirstOrDefault(p => p.Id == id);
            AddItemToWishList(product, amount);
        }
    }
}