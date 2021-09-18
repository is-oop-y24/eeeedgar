using System.Collections.Generic;
using System.Linq;
using Shops.Services;

namespace Shops.Entities
{
    public class Shop : BankClient, IShop
    {
        private List<Position> _stock;
        private Shop(string name, string address, IReadOnlyList<Product> permittedProducts, ShopManager shopManager)
        {
            Name = name;
            Address = address;
            _stock = new List<Position>();
            PermittedProducts = permittedProducts;
            ShopManager = shopManager;
        }

        public string Name { get; }
        public string Address { get; }

        // doubly connected for ui :[
        public IReadOnlyList<Product> PermittedProducts { get; }
        public ShopManager ShopManager { get; }
        public IReadOnlyList<Position> Stock => _stock;

        public static Shop CreateInstance(string name, string address, IReadOnlyList<Product> registeredProducts, ShopManager shopManager)
        {
            return new Shop(name, address, registeredProducts, shopManager);
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

        public bool CanSell(Product product, int amount)
        {
            Position position = FindPosition(product);
            return position.Amount > amount;
        }

        public int PossibleCost(IReadOnlyList<Purchase> wishList)
        {
            int cost = 0;
            foreach (Purchase purchase in wishList)
            {
                Position position = FindPosition(purchase.Product);
                cost += purchase.Amount * position.Cost;
            }

            return cost;
        }

        public void Sell(IReadOnlyList<Purchase> wishList)
        {
            foreach (Purchase purchase in wishList)
            {
                Position position = FindPosition(purchase.Product);
                position.Amount -= purchase.Amount;
            }
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