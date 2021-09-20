using System.Collections.Generic;
using System.Linq;
using Shops.Services;

namespace Shops.Entities
{
    public class Shop : BankClient, IShop
    {
        private List<Position> _stock;
        private Shop(string name, string address, IReadOnlyList<Product> permittedProducts)
        {
            Name = name;
            Address = address;
            _stock = new List<Position>();
            PermittedProducts = permittedProducts;
        }

        public string Name { get; }
        public string Address { get; }

        // doubly connected for ui :[
        public IReadOnlyList<Product> PermittedProducts { get; }
        public IReadOnlyList<Position> Stock => _stock;

        public static Shop CreateInstance(string name, string address, IReadOnlyList<Product> registeredProducts)
        {
            return new Shop(name, address, registeredProducts);
        }

        public void AddPosition(Product product)
        {
            var position = Position.CreateInstance(product);
            _stock.Add(position);
        }

        public void AddPosition(int id)
        {
            var position = Position.CreateInstance(FindProductInPermittedBase(id));
            _stock.Add(position);
        }

        public Position GetPosition(int id)
        {
            return _stock.Find(position => position.Product.Id == id);
        }

        public bool HasPosition(Product product)
        {
            return
                _stock
                    .Find(position => position.Product == product)
                != null;
        }

        public void AddProducts(Product product, int amount)
        {
            Position position = _stock.Find(pos => pos.Product == product);
            if (position != null)
                position.Amount += amount;
        }

        public void AddProducts(int id, int amount)
        {
            Product product = FindProductInPermittedBase(id);
            Position position = _stock.Find(pos => pos.Product == product);
            if (position != null)
                position.Amount += amount;
        }

        public void SetProductPrice(Product product, int price)
        {
            Position position = _stock.Find(pos => pos.Product == product);
            if (position != null)
                position.Cost = price;
        }

        public void SetProductPrice(int id, int price)
        {
            Product product = FindProductInPermittedBase(id);
            SetProductPrice(product, price);
        }

        public bool CanSell(Product product, int amount)
        {
            Position position = FindPosition(product);
            return position.Amount > amount;
        }

        public int Cost(IReadOnlyList<Purchase> wishList)
        {
            return wishList.Sum(purchase => Cost(purchase.Product, purchase.Amount));
        }

        public int Cost(Product product, int amount)
        {
            Position position = FindPosition(product);
            return position.Cost * amount;
        }

        public void Sell(IReadOnlyList<Purchase> wishList)
        {
            foreach (Purchase purchase in wishList)
            {
                Sell(purchase.Product, purchase.Amount);
            }
        }

        public void Sell(Product product, int amount)
        {
            Position position = FindPosition(product);
            position.Amount -= amount;
        }

        private Position FindPosition(Product product)
        {
            return
                _stock
                    .Find(pos => pos.Product.Equals(product));
        }

        private Product FindProductInPermittedBase(int id)
        {
            return PermittedProducts.FirstOrDefault(product => product.Id == id);
        }
    }
}