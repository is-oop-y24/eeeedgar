using Banks.Model.Accounts;

namespace Banks.Model.Transactions
{
    public class MoneyTransfer : ITransaction
    {
        private IBankAccount _sender;
        private IBankAccount _receiver;
        private int _money;
        public MoneyTransfer(IBankAccount sender, IBankAccount receiver, int money)
        {
            _sender = sender;
            _receiver = receiver;
            _money = money;
        }

        public void Make()
        {
            _sender.SendMoney(_money);
            _receiver.ReceiveMoney(_money);
        }

        public void Cancel()
        {
            throw new System.NotImplementedException();
        }
    }
}