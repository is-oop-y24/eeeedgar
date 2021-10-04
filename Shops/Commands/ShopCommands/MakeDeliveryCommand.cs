using Shops.Tools;
using Shops.UI;
using Spectre.Console;

namespace Shops.Commands.ShopCommands
{
    public class MakeDeliveryCommand : ICommand
    {
        public Context Execute(Context context)
        {
            ShopUi.DisplayStock(context.Shop.Name, context.Shop.Address, context.Shop.Stock);
            int productId = Clarifier.AskNumber("Product Id");
            int productAmount = Clarifier.AskNumber("Product Amount");
            AnsiConsole.Clear();
            context.Shop.AddProducts(productId, productAmount);
            return new Context(null, context.Shop, context.ShopManager);
        }
    }
}