using Banks.Model.Accounts;
using Banks.Model.Tools;

namespace Banks.Model.Transactions
{
    public class MoneyTransfer : ITransaction
    {
        private IBankAccount _sender;
        private IBankAccount _receiver;
        private decimal _money;
        private bool _isCompleted;
        private bool _isCanceled;
        public MoneyTransfer(IBankAccount sender, IBankAccount receiver, decimal money)
        {
            _sender = sender;
            _receiver = receiver;
            _money = money;
            _isCompleted = false;
            _isCanceled = false;
        }

        public void Commit()
        {
            if (_isCompleted)
                throw new BanksException("retry to commit a transaction");
            if (!_sender.IsConfirmed() && _money > _sender.BankingConditions().DoubtfulAccountLimit)
                throw new BanksException("exceeding the limit for doubtful accounts");
            _sender.DeductFunds(_money);
            _receiver.CreditFunds(_money);
            _isCompleted = true;
        }

        public void Cancel()
        {
            if (_isCanceled)
                throw new BanksException("retry to cancel a transaction");
            _receiver.DeductFunds(_money);
            _sender.CreditFunds(_money);
            _isCanceled = true;
        }
    }
}