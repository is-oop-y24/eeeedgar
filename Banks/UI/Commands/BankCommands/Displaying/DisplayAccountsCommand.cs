using Banks.UI.Controllers;
using Banks.UI.EntitiesUI;

namespace Banks.UI.Commands.BankCommands.Displaying
{
    public class DisplayAccountsCommand : ICommand
    {
        public Context Execute(Context context)
        {
            BankUi.DisplayAccounts(context.Bank.BankAccounts);
            return context;
        }
    }
}