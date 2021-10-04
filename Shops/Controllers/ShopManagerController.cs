using Shops.Commands.ShopManagerCommands;
using Shops.Tools;
using Shops.UI;

namespace Shops.Controllers
{
    public static class ShopManagerController
    {
        public static Context CheckShopManagerUiChoice(Context context)
        {
            string choice = ShopManagerUi.DisplayMenu();
            return choice switch
            {
                "Register Shop" => new RegisterShopCommand().Execute(context),
                "Register Product" => new RegisterProductCommand().Execute(context),
                "Register Customer" => new RegisterCustomerCommand().Execute(context),
                "Shop List" => new DisplayShopsCommand().Execute(context),
                "Customer List" => new DisplayCustomersCommand().Execute(context),
                "Product List" => new DisplayProductsCommand().Execute(context),
                "Select Shop" => new SelectShopCommand().Execute(context),
                "Select Customer" => new SelectCustomerCommand().Execute(context),
                "Exit" => new Context(null, null, null),
                _ => throw new ShopException("input error")
            };
        }
    }
}