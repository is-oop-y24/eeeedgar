using Shops.Entities;
using Shops.Tools;
using Shops.UI;
using Spectre.Console;

namespace Shops.Commands.CustomerCommands
{
    public class MakePurchaseCommand : ICommand
    {
        public Context Execute(Context context)
        {
            ShopManagerUi.DisplayShops(context.ShopManager.Shops);
            int shopId = Clarifier.AskNumber("Shop Id");
            AnsiConsole.Clear();
            Shop shop = context.ShopManager.GetShop(shopId);
            ShopUi.DisplayStock(shop.Name, shop.Address, shop.Stock);
            int productId = Clarifier.AskNumber("Product Id");
            int productAmount = Clarifier.AskNumber("Product Amount");
            Product product = context.ShopManager.GetProduct(productId);
            AnsiConsole.Clear();
            context.ShopManager.MakeDeal(context.Customer, shop, new Purchase(product, productAmount));
            return new Context(context.Customer, null, context.ShopManager);
        }
    }
}