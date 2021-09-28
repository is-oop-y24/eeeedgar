namespace Shops.Entities
{
    public interface IBank
    {
        void RegisterProfile(BankClient bankClient, int balance);
        int ProfileBalance(int clientId);
        void MakeTransaction(int senderId, int recipientId, int transactionValue);
    }
}