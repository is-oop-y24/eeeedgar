using System.Collections.Generic;
using System.Net;
using Shops.Entities;
using Shops.Services;
using Spectre.Console;

namespace Shops.UI
{
    public class ShopManagerUI
    {
        public static void Menu(ShopManager shopManager)
        {
            var commands = new List<string>();
            commands.Add("Create shop");
            commands.Add("Register product");
            commands.Add("Add customer");
            commands.Add("Shop List");
            commands.Add("Customer List");
            commands.Add("Product List");
            commands.Add("Select shop");
            commands.Add("Select customer");
            commands.Add("Bank");

            string choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("ShopManager Menu")
                    .PageSize(10)
                    .AddChoices(commands));
            AnsiConsole.Clear();

            switch (choice)
            {
                case "Create shop":
                {
                    string shopName = Clarifier.AskString("shop name");
                    string shopAddress = Clarifier.AskString("shop address");
                    shopManager.RegisterShop(shopName, shopAddress);
                    AnsiConsole.Clear();
                    Menu(shopManager);
                    break;
                }

                case "Register product":
                {
                    string productName = Clarifier.AskString("product name");
                    shopManager.RegisterProduct(productName);
                    AnsiConsole.Clear();
                    Menu(shopManager);
                    break;
                }

                case "Add customer":
                {
                    string customerName = Clarifier.AskString("customer name");
                    shopManager.RegisterPerson(customerName);
                    AnsiConsole.Clear();
                    Menu(shopManager);
                    break;
                }

                case "Shop List":
                {
                    DisplayShops(shopManager);
                    AnsiConsole.Confirm("yes");
                    AnsiConsole.Clear();
                    Menu(shopManager);
                    break;
                }

                case "Customer List":
                {
                    DisplayPersons(shopManager);
                    AnsiConsole.Confirm("yes");
                    AnsiConsole.Clear();
                    Menu(shopManager);
                    break;
                }

                case "Product List":
                {
                    DisplayProducts(shopManager);
                    AnsiConsole.Confirm("yes");
                    AnsiConsole.Clear();
                    Menu(shopManager);
                    break;
                }

                case "Select shop":
                {
                    DisplayShops(shopManager);
                    Shop shop = shopManager.GetShop(Clarifier.AskNumber("shop id"));
                    AnsiConsole.Clear();
                    ShopUI.Menu(shop);
                    break;
                }

                case "Select customer":
                {
                    DisplayPersons(shopManager);
                    Person person = shopManager.GetPerson(Clarifier.AskNumber("person id"));
                    AnsiConsole.Clear();
                    PersonUI.Menu(person);
                    break;
                }

                case "Bank":
                {
                    BankUI.Menu(shopManager.Bank);
                    break;
                }

                case "Exit":
                {
                    break;
                }
            }
        }

        private static void DisplayProducts(ShopManager shopManager)
        {
            var table = new Table();

            table.Title = new TableTitle("Registered Products");

            table.AddColumns("id", "Product Name");

            foreach (Product product in shopManager.Products)
            {
                table.AddRow(product.Id.ToString(), product.Name);
            }

            AnsiConsole.Render(table);
        }

        private static void DisplayShops(ShopManager shopManager)
        {
            var table = new Table();

            table.Title = new TableTitle("Registered Shops");

            table.AddColumns("id", "Shop Name", "Address");

            foreach (Shop shop in shopManager.Shops)
            {
                table.AddRow(shop.Id.ToString(), shop.Name, shop.Address);
            }

            AnsiConsole.Render(table);
        }

        private static void DisplayPersons(ShopManager shopManager)
        {
            var table = new Table();

            table.Title = new TableTitle("Registered Persons");

            table.AddColumns("id", "Person Name");

            foreach (Person person in shopManager.Persons)
            {
                table.AddRow(person.Id.ToString(), person.Name);
            }

            AnsiConsole.Render(table);
        }
    }
}