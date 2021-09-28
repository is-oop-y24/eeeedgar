namespace Shops.Entities
{
    public interface IShop
    {
        void AddPosition(int productId);
        void AddProducts(int productId, int productAmount);
        void SetProductPrice(int productId, int productPrice);
        bool CanSell(int productId, int productAmount);
        int PurchasePrice(int productId, int productAmount);
        void Sell(int productId, int productAmount);
    }
}