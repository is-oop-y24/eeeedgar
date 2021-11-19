using Banks.UI.Controllers;

namespace Banks.UI.Commands.BankClientCommands
{
    public class SetSubscriptionCommand : ICommand
    {
        private bool _needSubscribe;
        public SetSubscriptionCommand(bool needSubscribe)
        {
            _needSubscribe = needSubscribe;
        }

        public Context Execute(Context context)
        {
            if (_needSubscribe)
            {
                context.BankClient.Subscribe();
            }
            else
            {
                context.BankClient.Unsubscribe();
            }

            return context;
        }
    }
}