namespace Shops.Entities
{
    public class PaymentSystemUser
    {
        private static int _id;

        public PaymentSystemUser()
        {
            Id = ++_id;
        }

        public int Id { get; }
    }
}