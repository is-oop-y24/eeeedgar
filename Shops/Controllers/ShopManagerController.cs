using Shops.Entities;
using Shops.Services;
using Shops.Tools;
using Shops.UI;
using Spectre.Console;

namespace Shops.Controllers
{
    public class ShopManagerController
    {
        public static void CheckShopManagerUiChoice(string choice, ShopManager shopManager)
        {
            switch (choice)
            {
                case "Register Shop":
                {
                    RegisterShop(shopManager);
                    break;
                }

                case "Register Product":
                {
                    RegisterProduct(shopManager);
                    break;
                }

                case "Register Customer":
                {
                    RegisterCustomer(shopManager);
                    break;
                }

                case "Shop List":
                {
                    ShopList(shopManager);
                    break;
                }

                case "Customer List":
                {
                    CustomerList(shopManager);
                    break;
                }

                case "Product List":
                {
                    ProductList(shopManager);
                    break;
                }

                case "Select Shop":
                {
                    SelectShop(shopManager);
                    break;
                }

                case "Select Customer":
                {
                    SelectCustomer(shopManager);
                    break;
                }

                case "Exit":
                {
                    break;
                }

                default:
                {
                    throw new ShopException("input error");
                }
            }
        }

        private static void RegisterShop(ShopManager shopManager)
        {
            string shopName = Clarifier.AskString("Shop Name");
            string shopAddress = Clarifier.AskString("Shop Address");
            AnsiConsole.Clear();
            shopManager.RegisterShop(shopName, shopAddress);
            CheckShopManagerUiChoice(ShopManagerUi.DisplayMenu(), shopManager);
        }

        private static void RegisterProduct(ShopManager shopManager)
        {
            string productName = Clarifier.AskString("Product Name");
            AnsiConsole.Clear();
            GlobalProductBase.RegisterProduct(productName);
            CheckShopManagerUiChoice(ShopManagerUi.DisplayMenu(), shopManager);
        }

        private static void RegisterCustomer(ShopManager shopManager)
        {
            string customerName = Clarifier.AskString("Customer Name");
            int customerBalance = Clarifier.AskNumber("Customer Balance");
            AnsiConsole.Clear();
            shopManager.RegisterCustomer(customerName, customerBalance);
            CheckShopManagerUiChoice(ShopManagerUi.DisplayMenu(), shopManager);
        }

        private static void ShopList(ShopManager shopManager)
        {
            ShopManagerUi.DisplayShops(shopManager.Shops);
            AnsiConsole.Confirm("type to continue");
            AnsiConsole.Clear();
            CheckShopManagerUiChoice(ShopManagerUi.DisplayMenu(), shopManager);
        }

        private static void CustomerList(ShopManager shopManager)
        {
            ShopManagerUi.DisplayPersons(shopManager.Customers);
            AnsiConsole.Confirm("type to continue");
            AnsiConsole.Clear();
            CheckShopManagerUiChoice(ShopManagerUi.DisplayMenu(), shopManager);
        }

        private static void ProductList(ShopManager shopManager)
        {
            ShopManagerUi.DisplayProducts(GlobalProductBase.GetInstance());
            AnsiConsole.Confirm("type to continue");
            AnsiConsole.Clear();
            CheckShopManagerUiChoice(ShopManagerUi.DisplayMenu(), shopManager);
        }

        private static void SelectShop(ShopManager shopManager)
        {
            ShopManagerUi.DisplayShops(shopManager.Shops);
            int shopId = Clarifier.AskNumber("Shop Id");
            AnsiConsole.Clear();
            Shop shop = shopManager.GetShop(shopId);
            ShopController.CheckShopUiChoice(ShopUi.DisplayMenu(shop.Name, shop.Address), shop, shopManager);
        }

        private static void SelectCustomer(ShopManager shopManager)
        {
            ShopManagerUi.DisplayPersons(shopManager.Customers);
            int customerId = Clarifier.AskNumber("Customer Id");
            AnsiConsole.Clear();
            Customer customer = shopManager.GetCustomer(customerId);
            CustomerController.CheckCustomerUiChoice(PersonUi.DisplayMenu(customer.Name), customer, shopManager);
        }
    }
}