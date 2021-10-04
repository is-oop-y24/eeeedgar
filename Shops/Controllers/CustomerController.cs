using Shops.Commands.CustomerCommands;
using Shops.Tools;
using Shops.UI;

namespace Shops.Controllers
{
    public static class CustomerController
    {
        public static Context CheckCustomerUiChoice(Context context)
        {
            string choice = CustomerUi.DisplayMenu(context.Customer.Name);
            return choice switch
            {
                "Show Shop Stock" => new DisplayShopStockToCustomerCommand().Execute(context),
                "Buy" => new MakePurchaseCommand().Execute(context),
                "Back to Shop Manager" => new Context(null, null, context.ShopManager),
                _ => throw new ShopException("input error")
            };
        }
    }
}