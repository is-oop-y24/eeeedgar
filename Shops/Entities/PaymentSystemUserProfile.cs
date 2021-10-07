namespace Shops.Entities
{
    public class PaymentSystemUserProfile
    {
        public PaymentSystemUserProfile(PaymentSystemUser paymentSystemUser, int balance)
        {
            PaymentSystemUser = paymentSystemUser;
            Balance = balance;
        }

        public PaymentSystemUser PaymentSystemUser { get; }
        internal int Balance { get; set; }
    }
}