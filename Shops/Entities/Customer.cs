using Shops.Tools;

namespace Shops.Entities
{
    public class Customer : PaymentSystemUser
    {
        public Customer(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ShopException("wrong person name");
            Name = name;
        }

        public string Name { get; }
    }
}