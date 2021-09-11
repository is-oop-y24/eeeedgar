using System.Collections.Generic;
using Shops.Entities;

namespace Shops.Services
{
    public class MarketDatabase : IMarketDatabase
    {
        private List<Shop> _shops;
        private List<Product> _products;

        private MarketDatabase()
        {
            _shops = new List<Shop>();
            _products = new List<Product>();
        }

        // i can get an access to shops and use their methods.
        // do i want to control shops without TaxOffice.GetShop(int id)?
        // public IReadOnlyList<Shop> RegisteredShops => _shops; // do i need it?
        // public IReadOnlyList<Product> RegisteredProducts => _products; // and this. i'm not sure
        public static MarketDatabase CreateInstance()
        {
            return new MarketDatabase();
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
            var shop = Shop.CreateInstance(shopName, shopAddress);
            _shops.Add(shop);
            return shop;
        }

        public Product GetProduct(int id)
        {
            return _products.Find(product => product.Id == id);
        }

        public Shop GetShop(int id)
        {
            return _shops.Find(shop => shop.Id == id);
        }
    }
}