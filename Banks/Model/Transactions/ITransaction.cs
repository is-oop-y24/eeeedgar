namespace Banks.Model.Transactions
{
    public interface ITransaction
    {
        void Commit();
        void Cancel();
    }
}