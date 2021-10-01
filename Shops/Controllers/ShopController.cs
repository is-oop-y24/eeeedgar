using System;
using Shops.Entities;
using Shops.Services;
using Shops.UI;
using Spectre.Console;

namespace Shops.Controllers
{
    public class ShopController
    {
        public static void CheckShopUiChoice(string choice, Shop shop, ShopManager shopManager)
        {
            switch (choice)
            {
                case "Stock":
                {
                    Stock(shop, shopManager);
                    break;
                }

                case "Add Position":
                {
                    AddPositionToShop(shop, shopManager);
                    break;
                }

                case "Make Delivery":
                {
                    MakeDelivery(shop, shopManager);
                    break;
                }

                case "Set Price":
                {
                    SetPrice(shop, shopManager);
                    break;
                }

                case "Back to Shop Manager":
                {
                    BackToShopManager(shopManager);
                    break;
                }

                default:
                {
                    throw new Exception("input error");
                }
            }
        }

        private static void Stock(Shop shop, ShopManager shopManager)
        {
            ShopUi.DisplayStock(shop.Name, shop.Address, shop.Stock);
            AnsiConsole.Confirm("type to continue");
            AnsiConsole.Clear();
            CheckShopUiChoice(ShopUi.DisplayMenu(shop.Name, shop.Address), shop, shopManager);
        }

        private static void AddPositionToShop(Shop shop, ShopManager shopManager)
        {
            ShopManagerUi.DisplayProducts(GlobalProductBase.GetInstance());
            int productId = Clarifier.AskNumber("Product Id");
            AnsiConsole.Clear();
            shop.AddPosition(productId);
            CheckShopUiChoice(ShopUi.DisplayMenu(shop.Name, shop.Address), shop, shopManager);
        }

        private static void MakeDelivery(Shop shop, ShopManager shopManager)
        {
            ShopUi.DisplayStock(shop.Name, shop.Address, shop.Stock);
            int productId = Clarifier.AskNumber("Product Id");
            int productAmount = Clarifier.AskNumber("Product Amount");
            AnsiConsole.Clear();
            shop.AddProducts(productId, productAmount);
            CheckShopUiChoice(ShopUi.DisplayMenu(shop.Name, shop.Address), shop, shopManager);
        }

        private static void SetPrice(Shop shop, ShopManager shopManager)
        {
            ShopUi.DisplayStock(shop.Name, shop.Address, shop.Stock);
            int productId = Clarifier.AskNumber("Product Id");
            int productPrice = Clarifier.AskNumber("New Price");
            shop.SetProductPrice(productId, productPrice);
            AnsiConsole.Clear();
            CheckShopUiChoice(ShopUi.DisplayMenu(shop.Name, shop.Address), shop, shopManager);
        }

        private static void BackToShopManager(ShopManager shopManager)
        {
            AnsiConsole.Clear();
            ShopManagerController.CheckShopManagerUiChoice(ShopManagerUi.DisplayMenu(), shopManager);
        }
    }
}