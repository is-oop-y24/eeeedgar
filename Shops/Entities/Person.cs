using System.Collections.Generic;
using System.Dynamic;

namespace Shops.Entities
{
    public class Person : BankClient
    {
        private List<Purchase> _wishList;

        private Person(string name)
        {
            Name = name;
            _wishList = new List<Purchase>();
        }

        public string Name { get; }
        public IReadOnlyList<Purchase> WishList => _wishList;

        public static Person CreateInstance(string name)
        {
            return new Person(name);
        }

        public void AddItemToWishList(Product product, int amount = 1)
        {
            _wishList.Add(Purchase.CreateInstance(product, amount));
        }
    }
}