namespace Shops.Entities
{
    public class Purchase
    {
        public Purchase(Product product, int amount)
        {
            Product = product;
            Amount = amount;
        }

        public Product Product { get; }
        public int Amount { get; }
    }
}