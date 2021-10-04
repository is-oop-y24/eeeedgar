using Shops.Tools;
using Shops.UI;
using Spectre.Console;

namespace Shops.Commands.ShopManagerCommands
{
    public class DisplayCustomersCommand : ICommand
    {
        public Context Execute(Context context)
        {
            ShopManagerUi.DisplayPersons(context.ShopManager.Customers);
            AnsiConsole.Confirm("type to continue");
            AnsiConsole.Clear();
            return new Context(null, null, context.ShopManager);
        }
    }
}