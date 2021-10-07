using Shops.Tools;
using Shops.UI;
using Spectre.Console;

namespace Shops.Commands.ShopCommands
{
    public class DisplayStockCommand : ICommand
    {
        public Context Execute(Context context)
        {
            ShopUi.DisplayStock(context.Shop.Name, context.Shop.Address, context.Shop.Stock);
            AnsiConsole.Confirm("type to continue");
            AnsiConsole.Clear();
            return new Context(null, context.Shop, context.ShopManager);
        }
    }
}