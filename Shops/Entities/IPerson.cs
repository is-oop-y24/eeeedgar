namespace Shops.Entities
{
    public interface IPerson
    {
        void MakePurchase(int shopId, int productId, int productAmount);
    }
}