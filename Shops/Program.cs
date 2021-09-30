using Shops.Controllers;
using Shops.Services;

namespace Shops
{
    internal class Program
    {
        private static void Main()
        {
            var shopManager = new ShopManager();
            var controller = Controller.CreateInstance(shopManager);
            controller.Run();
        }
    }
}