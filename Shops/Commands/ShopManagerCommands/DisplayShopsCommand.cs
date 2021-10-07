using Shops.Tools;
using Shops.UI;
using Spectre.Console;

namespace Shops.Commands.ShopManagerCommands
{
    public class DisplayShopsCommand : ICommand
    {
        public Context Execute(Context context)
        {
            ShopManagerUi.DisplayShops(context.ShopManager.Shops);
            AnsiConsole.Confirm("type to continue");
            AnsiConsole.Clear();
            return new Context(null, null, context.ShopManager);
        }
    }
}