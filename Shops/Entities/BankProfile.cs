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
        public int Balance { get; set; }

        public static BankProfile CreateInstance(BankClient bankClient, int balance = 100)
        {
            return new BankProfile(bankClient, balance);
        }
    }
}