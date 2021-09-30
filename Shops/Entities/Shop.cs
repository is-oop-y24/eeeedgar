using System.Collections.Generic;
using System.Linq;
using Shops.Tools;

namespace Shops.Entities
{
    public class Shop : PaymentSystemUser
    {
        private List<StockPosition> _stock;
        public Shop(string name, string address)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ShopException("wrong shop name");
            if (string.IsNullOrWhiteSpace(address))
                throw new ShopException("wrong shop address");
            Name = name;
            Address = address;
            _stock = new List<StockPosition>();
        }

        public string Name { get; }
        public string Address { get; }

        public IReadOnlyList<StockPosition> Stock => _stock;

        public void AddPosition(int productId)
        {
            if (_stock.Any(pos => pos.Product.Id == productId))
                throw new ShopException("shop contains this position already");

            var position = new StockPosition(GlobalProductBase.GetProduct(productId));
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
            StockPosition stockPosition = _stock.Find(pos => pos.Product.Id == productId);
            if (stockPosition == null) throw new ShopException("wrong product id");
            stockPosition.Amount -= productAmount;
        }

        private StockPosition GetPositionInStock(int id)
        {
            return _stock.Find(pos => pos.Product.Id == id) ?? throw new ShopException("wrong position id");
        }
    }
}