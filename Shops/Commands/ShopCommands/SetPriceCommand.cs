using Shops.Tools;
using Shops.UI;
using Spectre.Console;

namespace Shops.Commands.ShopCommands
{
    public class SetPriceCommand : ICommand
    {
        public Context Execute(Context context)
        {
            ShopUi.DisplayStock(context.Shop.Name, context.Shop.Address, context.Shop.Stock);
            int productId = Clarifier.AskNumber("Product Id");
            int productPrice = Clarifier.AskNumber("New Price");
            context.Shop.SetProductPrice(productId, productPrice);
            AnsiConsole.Clear();
            return new Context(null, context.Shop, context.ShopManager);
        }
    }
}