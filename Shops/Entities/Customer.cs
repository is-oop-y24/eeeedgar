using Shops.Tools;

namespace Shops.Entities
{
    public class Customer : BankClient, ICustomer
    {
        private Customer(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ShopException("wrong person name");
            Name = name;
        }

        public string Name { get; }
        public static Customer CreateInstance(string name)
        {
            return new Customer(name);
        }
    }
}