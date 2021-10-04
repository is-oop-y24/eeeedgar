using Shops.Entities;
using Shops.Tools;
using Shops.UI;
using Spectre.Console;

namespace Shops.Commands.CustomerCommands
{
    public class DisplayShopStockToCustomerCommand : ICommand
    {
        public Context Execute(Context context)
        {
            ShopManagerUi.DisplayShops(context.ShopManager.Shops);
            int shopId = Clarifier.AskNumber("Shop Id");
            AnsiConsole.Clear();
            Shop shop = context.ShopManager.GetShop(shopId);
            ShopUi.DisplayStock(shop.Name, shop.Address, shop.Stock);
            AnsiConsole.Confirm("type to continue");
            AnsiConsole.Clear();
            return new Context(context.Customer, shop, context.ShopManager);
        }
    }
}