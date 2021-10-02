using Shops.Entities;
using Shops.Services;
using Shops.Tools;
using Shops.UI;
using Spectre.Console;

namespace Shops.Controllers
{
    public class CustomerController
    {
        public static void CheckCustomerUiChoice(string choice, Customer customer, ShopManager shopManager)
        {
            switch (choice)
            {
                case "Show Shop Stock":
                {
                    ShowShopStock(customer, shopManager);
                    break;
                }

                case "Buy":
                {
                    Buy(customer, shopManager);
                    break;
                }

                case "Back to Shop Manager":
                {
                    BackToShopManager(shopManager);
                    break;
                }

                default:
                {
                    throw new ShopException("input error");
                }
            }
        }

        private static void ShowShopStock(Customer customer, ShopManager shopManager)
        {
            ShopManagerUi.DisplayShops(shopManager.Shops);
            int shopId = Clarifier.AskNumber("Shop Id");
            AnsiConsole.Clear();
            Shop shop = shopManager.GetShop(shopId);
            ShopUi.DisplayStock(shop.Name, shop.Address, shop.Stock);
            AnsiConsole.Confirm("type to continue");
            AnsiConsole.Clear();
            CheckCustomerUiChoice(PersonUi.DisplayMenu(customer.Name), customer, shopManager);
        }

        private static void Buy(Customer customer, ShopManager shopManager)
        {
            ShopManagerUi.DisplayShops(shopManager.Shops);
            int shopId = Clarifier.AskNumber("Shop Id");
            AnsiConsole.Clear();
            Shop shop = shopManager.GetShop(shopId);
            ShopUi.DisplayStock(shop.Name, shop.Address, shop.Stock);
            int productId = Clarifier.AskNumber("Product Id");
            int productAmount = Clarifier.AskNumber("Product Amount");
            Product product = shopManager.GetProduct(productId);
            AnsiConsole.Clear();
            shopManager.MakeDeal(customer, shop, new Purchase(product, productAmount));
            CheckCustomerUiChoice(PersonUi.DisplayMenu(customer.Name), customer, shopManager);
        }

        private static void BackToShopManager(ShopManager shopManager)
        {
            AnsiConsole.Clear();
            ShopManagerController.CheckShopManagerUiChoice(ShopManagerUi.DisplayMenu(), shopManager);
        }
    }
}