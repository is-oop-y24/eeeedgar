using Shops.Services;
using Shops.Tools;

namespace Shops.Controllers
{
    public static class MainController
    {
        public static void Run(ShopManager shopManager)
        {
            var context = new Context(null, null, shopManager);
            while (context.ShopManager != null)
            {
                context = RunControllers(context);
            }
        }

        private static Context RunControllers(Context context)
        {
            if (context.Customer != null)
                return CustomerController.CheckCustomerUiChoice(context);
            if (context.Shop != null)
                return ShopController.CheckShopUiChoice(context);
            return ShopManagerController.CheckShopManagerUiChoice(context);
        }
    }
}