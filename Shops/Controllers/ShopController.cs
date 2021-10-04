using System;
using Shops.Commands.ShopCommands;
using Shops.Tools;
using Shops.UI;

namespace Shops.Controllers
{
    public static class ShopController
    {
        public static Context CheckShopUiChoice(Context context)
        {
            string choice = ShopUi.DisplayMenu(context.Shop.Name, context.Shop.Address);
            return choice switch
            {
                "Stock" => new DisplayStockCommand().Execute(context),
                "Add Position" => new AddPositionToShopCommand().Execute(context),
                "Make Delivery" => new MakeDeliveryCommand().Execute(context),
                "Set Price" => new SetPriceCommand().Execute(context),
                "Back to Shop Manager" => new Context(null, null, context.ShopManager),
                _ => throw new Exception("input error")
            };
        }
    }
}