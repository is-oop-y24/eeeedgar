using System.Collections.Generic;
using System.Linq;
using Shops.Tools;

namespace Shops.Entities
{
    public class Shop : BankClient, IShop
    {
        private List<Position> _stock;
        private Shop(string name, string address, IReadOnlyList<Product> globalProductBase)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ShopException("wrong shop name");
            if (string.IsNullOrWhiteSpace(address))
                throw new ShopException("wrong shop address");
            Name = name;
            Address = address;
            _stock = new List<Position>();
            GlobalProductBase = globalProductBase;
        }

        public string Name { get; }
        public string Address { get; }

        public IReadOnlyList<Position> Stock => _stock;
        private IReadOnlyList<Product> GlobalProductBase { get; }

        public static Shop CreateInstance(string name, string address, IReadOnlyList<Product> registeredProducts)
        {
            return new Shop(name, address, registeredProducts);
        }

        public void AddPosition(int productId)
        {
            if (_stock.Any(pos => pos.Product.Id == productId))
                throw new ShopException("shop contains this position already");

            var position = Position.CreateInstance(GetProductInGlobalBase(productId));
            _stock.Add(position);
        }

        public void AddProducts(int productId, int productAmount)
        {
            GetPositionInStock(productId).Amount += productAmount;
        }

        public void SetProductPrice(int productId, int productPrice)
        {
            GetPositionInStock(productId).Cost = productPrice;
        }

        public bool CanSell(int productId, int productAmount)
        {
            return GetPositionInStock(productId).Amount >= productAmount;
        }

        public int PurchasePrice(int productId, int productAmount)
        {
            return GetPositionInStock(productId).Cost * productAmount;
        }

        public void Sell(int productId, int productAmount)
        {
            Position position = _stock.Find(pos => pos.Product.Id == productId);
            if (position != null)
                position.Amount -= productAmount;
        }

        private Position GetPositionInStock(int id)
        {
            return _stock.Find(pos => pos.Product.Id == id) ?? throw new ShopException("wrong position id");
        }

        private Product GetProductInGlobalBase(int id)
        {
            return GlobalProductBase.FirstOrDefault(prod => prod.Id == id) ?? throw new ShopException("product is not in global base");
        }
    }
}