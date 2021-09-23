using System;
using System.Collections.Generic;
using System.Linq;

namespace Shops.Entities
{
    public class Shop : BankClient, IShop
    {
        private List<Position> _stock;
        private Shop(string name, string address, IReadOnlyList<Product> globalProductBase)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new Exception("wrong shop name");
            if (string.IsNullOrWhiteSpace(address))
                throw new Exception("wrong shop address");
            Name = name;
            Address = address;
            _stock = new List<Position>();
            GlobalProductBase = globalProductBase;
        }

        public string Name { get; }
        public string Address { get; }

        public IReadOnlyList<Product> GlobalProductBase { get; }
        public IReadOnlyList<Position> Stock => _stock;

        public static Shop CreateInstance(string name, string address, IReadOnlyList<Product> registeredProducts)
        {
            return new Shop(name, address, registeredProducts);
        }

        public void AddPosition(int id)
        {
            if (HasPosition(id))
                throw new Exception("shop contains this position already");

            var position = Position.CreateInstance(FindProductInGlobalGlobalBase(id));
            _stock.Add(position);
        }

        public bool HasPosition(int id)
        {
            return _stock.Find(position => position.Product.Id == id) != null;
        }

        public void AddProducts(int id, int amount)
        {
            FindPositionInStock(id).Amount += amount;
        }

        public void SetProductPrice(int id, int price)
        {
            FindPositionInStock(id).Cost = price;
        }

        public bool CanSell(int id, int amount)
        {
            return FindPositionInStock(id).Amount >= amount;
        }

        public int PurchaseCost(int id, int amount)
        {
            return FindPositionInStock(id).Cost * amount;
        }

        public void Sell(int id, int amount)
        {
            Position position = _stock.Find(pos => pos.Product.Id == id);
            if (position != null) position.Amount -= amount;
        }

        private Position FindPositionInStock(int id)
        {
            Position position = _stock.FirstOrDefault(pos => pos.Product.Id == id);
            if (position == null)
                throw new Exception("wrong position id___");
            return position;
        }

        private Product FindProductInGlobalGlobalBase(int id)
        {
            Product product = GlobalProductBase.FirstOrDefault(prod => prod.Id == id);
            if (product == null)
                throw new Exception("product is not in global base");
            return product;
        }
    }
}