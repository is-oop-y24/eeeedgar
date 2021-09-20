using Shops.Entities;
using Shops.Services;
using Shops.UI;
using Spectre.Console;
using Spectre.Console.Cli;

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
            CheckShopManagerUiChoice(ShopManagerUI.Menu());
        }

        private void CheckShopManagerUiChoice(string choice)
        {
            switch (choice)
            {
                case "Create shop":
                {
                    /*
                     * вынести три строки в функцию в UI
                     */
                    string shopName = Clarifier.AskString("shop name");
                    string shopAddress = Clarifier.AskString("shop address");
                    AnsiConsole.Clear();

                    _shopManager.RegisterShop(shopName, shopAddress);
                    CheckShopManagerUiChoice(ShopManagerUI.Menu());
                    break;
                }

                case "Register product":
                {
                    string productName = Clarifier.AskString("product name");
                    AnsiConsole.Clear();

                    _shopManager.RegisterProduct(productName);
                    CheckShopManagerUiChoice(ShopManagerUI.Menu());
                    break;
                }

                case "Add customer":
                {
                    string customerName = Clarifier.AskString("customer name");
                    AnsiConsole.Clear();

                    _shopManager.RegisterPerson(customerName);
                    CheckShopManagerUiChoice(ShopManagerUI.Menu());
                    break;
                }

                case "Shop List":
                {
                    ShopManagerUI.DisplayShops(_shopManager.Shops);
                    AnsiConsole.Confirm("type to exit");
                    AnsiConsole.Clear();

                    CheckShopManagerUiChoice(ShopManagerUI.Menu());
                    break;
                }

                case "Customer List":
                {
                    ShopManagerUI.DisplayPersons(_shopManager.Persons);
                    AnsiConsole.Confirm("type to exit");
                    AnsiConsole.Clear();

                    CheckShopManagerUiChoice(ShopManagerUI.Menu());
                    break;
                }

                case "Product List":
                {
                    ShopManagerUI.DisplayProducts(_shopManager.Products);
                    AnsiConsole.Confirm("type to exit");
                    AnsiConsole.Clear();

                    CheckShopManagerUiChoice(ShopManagerUI.Menu());
                    break;
                }

                case "Select shop":
                {
                    ShopManagerUI.DisplayShops(_shopManager.Shops);
                    _shop = _shopManager.GetShop(Clarifier.AskNumber("shop id"));
                    AnsiConsole.Clear();

                    CheckShopUiChoice(ShopUI.Menu(_shop.Name, _shop.Address));
                    break;
                }

                case "Select customer":
                {
                    ShopManagerUI.DisplayPersons(_shopManager.Persons);
                    _person = _shopManager.GetPerson(Clarifier.AskNumber("person id"));
                    AnsiConsole.Clear();

                    CheckPersonUiChoice(PersonUI.Menu(_person.Id, _person.Name));
                    break;
                }

                case "Bank":
                {
                    AnsiConsole.Clear();

                    // todo complete or reject BankUi
                    CheckBankUiChoice(BankUI.Menu());
                    break;
                }

                case "Exit":
                {
                    break;
                }
            }
        }

        private void CheckShopUiChoice(string choice)
        {
            switch (choice)
            {
                case "Stock":
                {
                    ShopUI.DisplayStock(_shop.Id, _shop.Name, _shop.Stock);
                    AnsiConsole.Confirm("type to exit");
                    AnsiConsole.Clear();
                    CheckShopUiChoice(ShopUI.Menu(_shop.Name, _shop.Address));
                    break;
                }

                case "Show Permitted Products":
                {
                    ShopManagerUI.DisplayProducts(_shopManager.Products);
                    AnsiConsole.Confirm("type to exit");
                    AnsiConsole.Clear();
                    CheckShopUiChoice(ShopUI.Menu(_shop.Name, _shop.Address));
                    break;
                }

                case "Add Position":
                {
                    ShopUI.DisplayPermittedProducts(_shop.Id, _shop.Name, _shop.PermittedProducts);
                    _shop.AddPosition(Clarifier.AskNumber("product id"));
                    AnsiConsole.Clear();
                    CheckShopUiChoice(ShopUI.Menu(_shop.Name, _shop.Address));
                    break;
                }

                case "Make Delivery":
                {
                    ShopUI.DisplayStock(_shop.Id, _shop.Name, _shop.Stock);
                    _shop.AddProducts(Clarifier.AskNumber("product id"), Clarifier.AskNumber("product amount"));
                    AnsiConsole.Clear();
                    CheckShopUiChoice(ShopUI.Menu(_shop.Name, _shop.Address));
                    break;
                }

                case "Set Price":
                {
                    ShopUI.DisplayStock(_shop.Id, _shop.Name, _shop.Stock);
                    _shop.SetProductPrice(Clarifier.AskNumber("product id"), Clarifier.AskNumber("price"));
                    AnsiConsole.Clear();
                    CheckShopUiChoice(ShopUI.Menu(_shop.Name, _shop.Address));
                    break;
                }

                case "Back to Shop Manager":
                {
                    CheckShopManagerUiChoice(ShopManagerUI.Menu());
                    break;
                }
            }
        }

        private void CheckPersonUiChoice(string choice)
        {
            switch (choice)
            {
                case "Money":
                {
                    AnsiConsole.WriteLine($"Money: {_person.Money}");
                    AnsiConsole.Confirm("type to exit");
                    AnsiConsole.Clear();
                    CheckPersonUiChoice(PersonUI.Menu(_person.Id, _person.Name));
                    break;
                }

                case "Wishlist":
                {
                    PersonUI.DisplayWishList(_person.Id, _person.Name, _person.WishList);
                    AnsiConsole.Confirm("type to exit");
                    AnsiConsole.Clear();
                    CheckPersonUiChoice(PersonUI.Menu(_person.Id, _person.Name));
                    break;
                }

                case "Add Item to Wishlist":
                {
                    ShopManagerUI.DisplayProducts(_shopManager.Products);
                    _person.AddItemToWishList(Clarifier.AskNumber("product id"), Clarifier.AskNumber("amount"));
                    AnsiConsole.Clear();
                    CheckPersonUiChoice(PersonUI.Menu(_person.Id, _person.Name));
                    break;
                }

                case "Back to Shop Manager":
                {
                    CheckShopManagerUiChoice(ShopManagerUI.Menu());
                    break;
                }

                case "Buy":
                {
                    ShopManagerUI.DisplayShops(_shopManager.Shops);
                    int shopId = Clarifier.AskNumber("shop id");
                    AnsiConsole.Clear();
                    Shop shop = _shopManager.GetShop(shopId);
                    ShopUI.DisplayStock(shop.Id, shop.Name, shop.Stock);
                    AnsiConsole.WriteLine("Money: ", _person.Money);
                    int productId = Clarifier.AskNumber("product id");
                    int productAmount = Clarifier.AskNumber("amount");
                    Product product = _shopManager.GetProduct(productId);
                    _person.Buy(shop, product, productAmount);
                    AnsiConsole.Clear();
                    CheckPersonUiChoice(PersonUI.Menu(_person.Id, _person.Name));
                    break;
                }
            }
        }

        private void CheckBankUiChoice(string choice)
        {
            switch (choice)
            {
                case "Profiles":
                {
                    BankUI.DisplayProfiles(_shopManager.Bank);
                    AnsiConsole.Confirm("type to exit");
                    AnsiConsole.Clear();
                    CheckBankUiChoice(BankUI.Menu());
                    break;
                }

                case "Back to Shop Manager":
                {
                    CheckShopManagerUiChoice(ShopManagerUI.Menu());
                    break;
                }
            }
        }
    }
}