using System.Collections.Generic;
using Shops.Entities;

namespace Shops.Services
{
    public class ShopManager : IShopManager
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
            _bank = Bank.CreateInstance(this);
        }

        // i can get an access to shops and use their methods.
        // do i want to control shops without TaxOffice.GetShop(int id)?
        // public IReadOnlyList<Shop> RegisteredShops => _shops; // do i need it?
        // public IReadOnlyList<Product> RegisteredProducts => _products; // and this. i'm not sure
        public IReadOnlyList<Shop> Shops => _shops;
        public IReadOnlyList<Person> Persons => _persons;
        public IReadOnlyList<Product> Products => _products;
        public Bank Bank => _bank;
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
            var shop = Shop.CreateInstance(shopName, shopAddress, _products, this);
            _shops.Add(shop);

            _bank.RegisterProfile(shop);
            return shop;
        }

        public Person RegisterPerson(string name)
        {
            var person = Person.CreateInstance(name, this);
            _persons.Add(person);

            _bank.RegisterProfile(person);
            return person;
        }

        public Product GetProduct(int id)
        {
            return _products.Find(product => product.Id == id);
        }

        public Shop GetShop(int id)
        {
            return _shops.Find(shop => shop.Id == id);
        }

        public Person GetPerson(int id)
        {
            return _persons.Find(person => person.Id == id);
        }

        /*
        public void MakeDeal(Person person, Shop shop)
        {
            int cost = shop.PossibleCost(person.WishList);
            if (!_bank.IsTransactionPossible(person, cost)) return;
            _bank.MakeTransaction(person.Id, shop.Id, cost);
            shop.Sell(person.WishList);
        }

        public void MakeDeal(int personId, int shopId)
        {
            Person person = _persons.Find(p => p.Id == personId);
            Shop shop = _shops.Find(s => s.Id == shopId);
            MakeDeal(person, shop);
        }
        */
    }
}