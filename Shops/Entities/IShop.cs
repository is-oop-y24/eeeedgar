namespace Shops.Entities
{
    public interface IShop
    {
        void AddPosition(int id);
        bool HasPosition(int id);
        void AddProducts(int id, int amount);
        void SetProductPrice(int id, int price);
        bool CanSell(int id, int amount);
        int PurchaseCost(int id, int amount);
        void Sell(int id, int amount);
    }
}