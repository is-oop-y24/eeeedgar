namespace Banks.Model.Transactions
{
    public interface ITransaction
    {
        void Make();
        void Cancel();
    }
}