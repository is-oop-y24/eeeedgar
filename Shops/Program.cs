using Shops.Controllers;
using Shops.Services;

namespace Shops
{
    internal class Program
    {
        private static void Main()
        {
            var shopManager = new ShopManager();
            MainController.Run(shopManager);
        }
    }
}