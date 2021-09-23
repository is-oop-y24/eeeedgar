namespace Shops.Entities
{
    public interface IBank
    {
        bool MakeTransaction(int senderId, int recipientId, int transactionValue);
        void RegisterProfile(BankClient bankClient);
        int ProfileBalance(int id);
    }
}