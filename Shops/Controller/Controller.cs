using System;
using Shops.Entities;
using Shops.Services;
using Shops.UI;
using Spectre.Console;

namespace Shops.Controller
{
    public class Controller
    {
        private ShopManager _shopManager;
        private Shop _shop;
        private Person _person;

        private Controller(ShopManager shopManager)
        {
            _shopManager = shopManager;
        }

        public static Controller CreateInstance(ShopManager shopManager)
        {
            return new Controller(shopManager);
        }

        public void Run()
        {
            CheckShopManagerUiChoice(ShopManagerUi.Menu());
        }

        private void CheckShopManagerUiChoice(string choice)
        {
            switch (choice)
            {
                case "Register Shop":
                {
                    string shopName = Clarifier.AskString("Shop Name");
                    string shopAddress = Clarifier.AskString("Shop Address");
                    AnsiConsole.Clear();
                    _shopManager.RegisterShop(shopName, shopAddress);
                    CheckShopManagerUiChoice(ShopManagerUi.Menu());
                    break;
                }

                case "Register Product":
                {
                    string productName = Clarifier.AskString("Product Name");
                    AnsiConsole.Clear();
                    _shopManager.RegisterProduct(productName);
                    CheckShopManagerUiChoice(ShopManagerUi.Menu());
                    break;
                }

                case "Register Customer":
                {
                    string customerName = Clarifier.AskString("Customer Name");
                    AnsiConsole.Clear();
                    _shopManager.RegisterPerson(customerName);
                    CheckShopManagerUiChoice(ShopManagerUi.Menu());
                    break;
                }

                case "Shop List":
                {
                    ShopManagerUi.DisplayShops(_shopManager.Shops);
                    AnsiConsole.Confirm("type to continue");
                    AnsiConsole.Clear();
                    CheckShopManagerUiChoice(ShopManagerUi.Menu());
                    break;
                }

                case "Customer List":
                {
                    ShopManagerUi.DisplayPersons(_shopManager.Persons);
                    AnsiConsole.Confirm("type to continue");
                    AnsiConsole.Clear();
                    CheckShopManagerUiChoice(ShopManagerUi.Menu());
                    break;
                }

                case "Product List":
                {
                    ShopManagerUi.DisplayProducts(_shopManager.Products);
                    AnsiConsole.Confirm("type to continue");
                    AnsiConsole.Clear();
                    CheckShopManagerUiChoice(ShopManagerUi.Menu());
                    break;
                }

                case "Select Shop":
                {
                    ShopManagerUi.DisplayShops(_shopManager.Shops);
                    int shopId = Clarifier.AskNumber("Shop Id");
                    AnsiConsole.Clear();
                    _shop = _shopManager.GetShop(shopId);
                    CheckShopUiChoice(ShopUi.Menu(_shop.Name, _shop.Address));
                    break;
                }

                case "Select Customer":
                {
                    ShopManagerUi.DisplayPersons(_shopManager.Persons);
                    int customerId = Clarifier.AskNumber("Customer Id");
                    AnsiConsole.Clear();
                    _person = _shopManager.GetPerson(customerId);
                    CheckPersonUiChoice(PersonUi.Menu(_person.Name, _person.Money));
                    break;
                }

                case "Exit":
                {
                    break;
                }

                default:
                {
                    throw new Exception("input error");
                }
            }
        }

        private void CheckShopUiChoice(string choice)
        {
            switch (choice)
            {
                case "Stock":
                {
                    ShopUi.DisplayStock(_shop.Name, _shop.Address, _shop.Stock);
                    AnsiConsole.Confirm("type to continue");
                    AnsiConsole.Clear();
                    CheckShopUiChoice(ShopUi.Menu(_shop.Name, _shop.Address));
                    break;
                }

                case "Add Position":
                {
                    ShopManagerUi.DisplayProducts(_shopManager.Products);
                    int productId = Clarifier.AskNumber("Product Id");
                    AnsiConsole.Clear();
                    _shop.AddPosition(productId);
                    CheckShopUiChoice(ShopUi.Menu(_shop.Name, _shop.Address));
                    break;
                }

                case "Make Delivery":
                {
                    ShopUi.DisplayStock(_shop.Name, _shop.Address, _shop.Stock);
                    int productId = Clarifier.AskNumber("Product Id");
                    int productAmount = Clarifier.AskNumber("Product Amount");
                    AnsiConsole.Clear();
                    _shop.AddProducts(productId, productAmount);
                    CheckShopUiChoice(ShopUi.Menu(_shop.Name, _shop.Address));
                    break;
                }

                case "Set Price":
                {
                    ShopUi.DisplayStock(_shop.Name, _shop.Address, _shop.Stock);
                    int productId = Clarifier.AskNumber("Product Id");
                    int productPrice = Clarifier.AskNumber("New Price");
                    _shop.SetProductPrice(productId, productPrice);
                    AnsiConsole.Clear();
                    CheckShopUiChoice(ShopUi.Menu(_shop.Name, _shop.Address));
                    break;
                }

                case "Back to Shop Manager":
                {
                    AnsiConsole.Clear();
                    CheckShopManagerUiChoice(ShopManagerUi.Menu());
                    break;
                }

                default:
                {
                    throw new Exception("input error");
                }
            }
        }

        private void CheckPersonUiChoice(string choice)
        {
            switch (choice)
            {
                case "Show Shop Stock":
                {
                    int shopId = Clarifier.AskNumber("Shop Id");
                    AnsiConsole.Clear();
                    _shop = _shopManager.GetShop(shopId);
                    ShopUi.DisplayStock(_shop.Name, _shop.Address, _shop.Stock);
                    AnsiConsole.Confirm("type to continue");
                    AnsiConsole.Clear();
                    CheckPersonUiChoice(PersonUi.Menu(_person.Name, _person.Money));
                    break;
                }

                case "Buy":
                {
                    ShopManagerUi.DisplayShops(_shopManager.Shops);
                    int shopId = Clarifier.AskNumber("Shop Id");
                    AnsiConsole.Clear();
                    Shop shop = _shopManager.GetShop(shopId);
                    ShopUi.DisplayStock(shop.Name, shop.Address, shop.Stock);
                    int productId = Clarifier.AskNumber("Product Id");
                    int productAmount = Clarifier.AskNumber("Product Amount");
                    AnsiConsole.Clear();
                    _person.MakePurchase(shopId, productId, productAmount);
                    CheckPersonUiChoice(PersonUi.Menu(_person.Name, _person.Money));
                    break;
                }

                case "Back to Shop Manager":
                {
                    AnsiConsole.Clear();
                    CheckShopManagerUiChoice(ShopManagerUi.Menu());
                    break;
                }

                default:
                {
                    throw new Exception("input error");
                }
            }
        }
    }
}