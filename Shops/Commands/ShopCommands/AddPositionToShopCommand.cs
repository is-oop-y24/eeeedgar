using Shops.Entities;
using Shops.Tools;
using Shops.UI;
using Spectre.Console;

namespace Shops.Commands.ShopCommands
{
    public class AddPositionToShopCommand : ICommand
    {
        public Context Execute(Context context)
        {
            ShopManagerUi.DisplayProducts(GlobalProductBase.GetInstance());
            int productId = Clarifier.AskNumber("Product Id");
            AnsiConsole.Clear();
            context.Shop.AddPosition(productId);
            return new Context(null, context.Shop, context.ShopManager);
        }
    }
}