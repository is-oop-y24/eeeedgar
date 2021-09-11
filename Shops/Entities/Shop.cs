using System.Collections.Generic;

namespace Shops.Entities
{
    public class Shop : IShop
    {
        private static int _lastCreatedShopId;
        private List<Position> _positions;
        private Shop(int id, string name, string address)
        {
            Id = id;
            Name = name;
            Address = address;
            _positions = new List<Position>();
        }

        public int Id { get; }
        public string Name { get; }
        public string Address { get; }

        public IReadOnlyList<Position> Positions => _positions;

        public static Shop CreateInstance(string name, string address)
        {
            return new Shop(++_lastCreatedShopId, name, address);
        }

        public void AddPosition(Product product)
        {
            var position = Position.CreateInstance(product);
            _positions.Add(position);
        }

        public bool HasPosition(Product product)
        {
            return
                _positions
                    .Find(position => position.Product == product)
                != null;
        }

        public void AddProducts(Product product, int amount)
        {
            Position position = _positions.Find(pos => pos.Product == product);
            if (position != null)
                position.Amount += amount;
        }

        public void SetProductPrice(Product product, int price)
        {
            Position position = _positions.Find(pos => pos.Product == product);
            if (position != null)
                position.Cost = price;
        }

        private Position FindPosition(Product product)
        {
            return
                _positions
                    .Find(pos => pos.Product == product);
        }
    }
}