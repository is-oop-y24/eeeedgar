using Banks.Model.Accounts;
using Banks.Model.Tools;

namespace Banks.Model.Transactions
{
    public class FundsWithdrawal : ITransaction
    {
        private IBankAccount _bankAccount;
        private decimal _money;
        private bool _isCompleted;
        private bool _isCanceled;

        public FundsWithdrawal(IBankAccount bankAccount, decimal money)
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
            if (!_bankAccount.IsConfirmed() && _money > _bankAccount.BankingConditions().DoubtfulAccountLimit)
                throw new BanksException("exceeding the limit for doubtful accounts");
            _bankAccount.DeductFunds(_money);
            _isCompleted = true;
        }

        public void Cancel()
        {
            if (_isCanceled)
                throw new BanksException("retry to cancel a transaction");
            _bankAccount.CreditFunds(_money);
            _isCanceled = true;
        }
    }
}