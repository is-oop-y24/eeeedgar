using Banks.Model.Accounts;

namespace Banks.Model.Transactions
{
    public class AccountReplenishment : ITransaction
    {
        private IBankAccount _bankAccount;
        private decimal _money;
        public AccountReplenishment(IBankAccount bankAccount, decimal money)
        {
            _bankAccount = bankAccount;
            _money = money;
        }

        public void Make()
        {
            _bankAccount.SendMoney(_money);
        }

        public void Cancel()
        {
            throw new System.NotImplementedException();
        }
    }
}