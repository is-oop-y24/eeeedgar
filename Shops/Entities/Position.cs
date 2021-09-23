namespace Shops.Entities
{
    public class Position
    {
        private Position(Product product)
        {
            Product = product;
            Cost = 0;
            Amount = 0;
        }

        public Product Product { get; }
        public int Cost { get; set; }
        public int Amount { get; set; }

        public static Position CreateInstance(Product product)
        {
            return new Position(product);
        }
    }
}