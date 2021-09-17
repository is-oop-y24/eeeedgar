namespace Shops.Entities
{
    public class BankClient
    {
        private static int _id;

        public BankClient()
        {
            Id = ++_id;
        }

        public int Id { get; }
    }
}