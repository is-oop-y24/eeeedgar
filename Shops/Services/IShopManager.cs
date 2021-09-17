using Shops.Entities;

namespace Shops.Services
{
    public interface IShopManager
    {
        Product RegisterProduct(string productName);
        Shop RegisterShop(string shopName, string shopAddress);
        Product GetProduct(int id);
        Shop GetShop(int id);
    }
}