using Shops.Entities;

namespace Shops.Services
{
    public interface IShopManager
    {
        Product CreateProduct(string productName);
        Shop CreateShop(string shopName, string shopAddress);
        Customer CreateCustomer(string name);
        Product GetProduct(int id);
        Shop GetShop(int id);
        Customer GetPerson(int id);
    }
}