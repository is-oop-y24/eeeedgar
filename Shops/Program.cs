using Shops.Services;

namespace Shops
{
    internal class Program
    {
        private static void Main()
        {
            var market = ShopManager.CreateInstance();

            var controller = Controller.Controller.CreateInstance(market);

            controller.Run();
        }
    }
}