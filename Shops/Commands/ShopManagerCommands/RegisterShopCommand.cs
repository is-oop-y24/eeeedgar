using Shops.Tools;
using Shops.UI;
using Spectre.Console;

namespace Shops.Commands.ShopManagerCommands
{
    public class RegisterShopCommand : ICommand
    {
        public Context Execute(Context context)
        {
            string shopName = Clarifier.AskString("Shop Name");
            string shopAddress = Clarifier.AskString("Shop Address");
            AnsiConsole.Clear();
            context.ShopManager.RegisterShop(shopName, shopAddress);
            return new Context(null, null, context.ShopManager);
        }
    }
}