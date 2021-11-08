using Banks.UI.EntitiesUI;
using Banks.UI.Tools;
using Spectre.Console;

namespace Banks.UI.Commands.CentralBankCommands.Displaying
{
    public class DisplayBanksCommand : ICommand
    {
        public Context Execute(Context context)
        {
            CentralBankUi.DisplayBanks(context.CentralBank.Banks);
            AnsiConsole.Confirm("type to continue");
            AnsiConsole.Clear();
            return context;
        }
    }
}