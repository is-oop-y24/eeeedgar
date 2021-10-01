using System.Text;
using Shops.Services;
using Shops.UI;

namespace Shops.Controllers
{
    public class MainController
    {
        public static void Run(ShopManager shopManager)
        {
            ShopManagerController.CheckShopManagerUiChoice(ShopManagerUi.DisplayMenu(), shopManager);
        }
    }
}