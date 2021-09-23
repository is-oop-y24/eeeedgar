using System;
using Shops.Entities;
using Shops.Services;

namespace Shops
{
    internal class Program
    {
        private static void Main()
        {
            var shopManager = ShopManager.CreateInstance();
            var controller = Controller.Controller.CreateInstance(shopManager);
            controller.Run();
        }
    }
}