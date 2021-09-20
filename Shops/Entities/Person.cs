using System.Collections.Generic;
using System.Linq;

namespace Shops.Entities
{
    public class Person : BankClient
    {
        private List<Purchase> _wishList;
        private Bank _bank;

        private Person(string name, IReadOnlyList<Product> permittedProducts, Bank bank)
        {
            Name = name;
            _wishList = new List<Purchase>();
            _bank = bank;
            PermittedProducts = permittedProducts;
        }

        public string Name { get; }
        public IReadOnlyList<Purchase> WishList => _wishList;
        public IReadOnlyList<Product> PermittedProducts { get; }

        public int Money => _bank.ProfileBalance(Id);

        public static Person CreateInstance(string name, IReadOnlyList<Product> permittedProducts, Bank bank)
        {
            return new Person(name, permittedProducts, bank);
        }

        public void Buy(Shop shop, Product product, int amount)
        {
            if (!shop.HasPosition(product))
                return;
            if (!shop.CanSell(product, amount))
                return;
            if (_bank.MakeTransaction(Id, shop.Id, shop.Cost(product, amount)))
                shop.Sell(product, amount);
        }

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