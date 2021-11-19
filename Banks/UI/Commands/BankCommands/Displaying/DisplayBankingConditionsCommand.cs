using Banks.UI.Controllers;
using Banks.UI.EntitiesUI;

namespace Banks.UI.Commands.BankCommands.Displaying
{
    public class DisplayBankingConditionsCommand : ICommand
    {
        public Context Execute(Context context)
        {
            BankUi.DisplayConditions(context.Bank.Conditions);
            return context;
        }
    }
}