using Banks.Model.Accounts;
using Banks.Model.Tools;

namespace Banks.Model.Transactions
{
    public class AccountReplenishment : ITransaction
    {
        private IBankAccount _bankAccount;
        private decimal _money;
        private bool _isCompleted;
        private bool _isCanceled;
        public AccountReplenishment(IBankAccount bankAccount, decimal money)
        {
            _bankAccount = bankAccount;
            _money = money;
            _isCompleted = false;
            _isCanceled = false;
        }

        public void Commit()
        {
            if (_isCompleted)
                throw new BanksException("retry to commit a transaction");
            _bankAccount.CreditFunds(_money);
            _isCompleted = true;
        }

        public void Cancel()
        {
            if (_isCanceled)
                throw new BanksException("retry to cancel a transaction");
            _bankAccount.DeductFunds(_money);
            _isCanceled = true;
        }
    }
}