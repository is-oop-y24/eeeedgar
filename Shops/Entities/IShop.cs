using Shops.Entities;

namespace Shops.Entities
{
    public interface IShop
    {
        void AddPosition(Product product);
        bool HasPosition(Product product);
        void AddProducts(Product product, int amount);
        void SetProductPrice(Product product, int price);
    }
}