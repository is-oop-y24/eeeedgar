using Shops.Entities;

namespace Shops.Services
{
    public interface IShopManager
    {
        Shop RegisterShop(string shopName, string shopAddress);
        Customer RegisterCustomer(string name, int balance);
        Product GetProduct(int productId);
        Shop GetShop(int shopId);
        Customer GetCustomer(int customerId);
        public void MakeDeal(Customer customer, Shop shop, Purchase purchase);
    }
}