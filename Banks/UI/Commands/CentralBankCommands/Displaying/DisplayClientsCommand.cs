using Banks.UI.Controllers;
using Banks.UI.EntitiesUI;
using Spectre.Console;

namespace Banks.UI.Commands.CentralBankCommands.Displaying
{
    public class DisplayClientsCommand : ICommand
    {
        public Context Execute(Context context)
        {
            CentralBankUi.DisplayClients(context.CentralBank.Clients);
            AnsiConsole.Confirm("type to continue");
            AnsiConsole.Clear();
            return context;
        }
    }
}