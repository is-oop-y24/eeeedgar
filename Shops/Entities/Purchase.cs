namespace Shops.Entities
{
    public class Purchase
    {
        private Purchase(Product product, int amount)
        {
            Product = product;
            Amount = amount;
        }

        public Product Product { get; }
        public int Amount { get; }

        public static Purchase CreateInstance(Product product, int amount)
        {
            return new Purchase(product, amount);
        }
    }
}