namespace Shops.Entities
{
    public class Product
    {
        private static int _lastCreatedProductId;
        private Product(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; }
        public string Name { get; }

        public static Product CreateInstance(string name)
        {
            return new Product(++_lastCreatedProductId, name);
        }
    }
}