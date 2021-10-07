namespace Shops.Entities
{
    public class Product
    {
        private static int _lastCreatedProductId;
        public Product(string name)
        {
            Id = ++_lastCreatedProductId;
            Name = name;
        }

        public int Id { get; }
        public string Name { get; }
    }
}