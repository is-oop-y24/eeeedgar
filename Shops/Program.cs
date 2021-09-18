using Shops.Entities;
using Shops.Services;
using Shops.UI;
using Spectre.Console;

namespace Shops
{
    internal class Program
    {
        private static void Main()
        {
            var market = ShopManager.CreateInstance();

            ShopManagerUI.Menu(market);
        }
    }
}