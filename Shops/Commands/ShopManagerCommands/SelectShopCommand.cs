using Shops.Entities;
using Shops.Tools;
using Shops.UI;
using Spectre.Console;

namespace Shops.Commands.ShopManagerCommands
{
    public class SelectShopCommand : ICommand
    {
        public Context Execute(Context context)
        {
            ShopManagerUi.DisplayShops(context.ShopManager.Shops);
            int shopId = Clarifier.AskNumber("Shop Id");
            AnsiConsole.Clear();
            Shop shop = context.ShopManager.GetShop(shopId);
            return new Context(null, shop, context.ShopManager);
        }
    }
}