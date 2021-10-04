using Shops.Entities;
using Shops.Tools;
using Shops.UI;
using Spectre.Console;

namespace Shops.Commands.ShopManagerCommands
{
    public class DisplayProductsCommand : ICommand
    {
        public Context Execute(Context context)
        {
            ShopManagerUi.DisplayProducts(GlobalProductBase.GetInstance());
            AnsiConsole.Confirm("type to continue");
            AnsiConsole.Clear();
            return new Context(null, null, context.ShopManager);
        }
    }
}