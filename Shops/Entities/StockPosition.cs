namespace Shops.Entities
{
    public class StockPosition
    {
        public StockPosition(Product product)
        {
            Product = product;
            Cost = 0;
            Amount = 0;
        }

        public Product Product { get; }
        public int Cost { get; internal set; }
        public int Amount { get; internal set; }
    }
}