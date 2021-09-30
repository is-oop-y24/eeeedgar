using System.Collections.Generic;
using System.Linq;
using Shops.Tools;

namespace Shops.Entities
{
    public class GlobalProductBase
    {
        private static List<Product> _products;

        public static IReadOnlyList<Product> GetInstance()
        {
            if (_products != null) return _products;
            _products = new List<Product>();
            return _products;
        }

        public static Product RegisterProduct(string name)
        {
            if (_products == null)
                _products = new List<Product>();
            if (_products.Any(product => product.Name.Equals(name)))
                throw new ShopException("can't add already existing product");
            var product = new Product(name);
            _products.Add(product);
            return product;
        }

        public static Product GetProduct(int productId)
        {
            if (_products == null)
                _products = new List<Product>();
            return _products.Find(product => product.Id == productId) ?? throw new ShopException("wrong product id");
        }

        public static Product FindProduct(string productName)
        {
            if (_products == null)
                _products = new List<Product>();
            return _products.Find(product => product.Name == productName);
        }
    }
}