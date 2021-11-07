using Banks.Model.Accounts;

namespace Banks.Model.Transactions
{
    public class FundsWithdrawal : ITransaction
    {
        private IBankAccount _bankAccount;
        private decimal _money;

        public FundsWithdrawal(IBankAccount bankAccount, decimal money)
        {
            _bankAccount = bankAccount;
            _money = money;
        }

        public void Make()
        {
            _bankAccount.ReceiveMoney(_money);
        }

        public void Cancel()
        {
            throw new System.NotImplementedException();
        }
    }
}