namespace Banks.Transactions
{
    public interface ITransaction
    {
        void Make();
        void Cancel();
    }
}