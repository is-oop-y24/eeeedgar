namespace Shops.Entities
{
    public class BankProfile
    {
        private BankProfile(BankClient bankClient, int balance)
        {
            BankClient = bankClient;
            Balance = balance;
        }

        public BankClient BankClient { get; }
        internal int Balance { get; set; }

        public static BankProfile CreateInstance(BankClient bankClient, int balance)
        {
            return new BankProfile(bankClient, balance);
        }
    }
}