using System.Collections.Generic;
using Shops.Entities;
using Spectre.Console;

namespace Shops.UI
{
    public class ShopManagerUi
    {
        public static string DisplayMenu()
        {
            var commands = new List<string>
            {
                "Register Shop",
                "Register Product",
                "Register Customer",

                "Shop List",
                "Customer List",
                "Product List",

                "Select Shop",
                "Select Customer",

                "Exit",
            };

            string choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("ShopManager Menu")
                    .PageSize(10)
                    .AddChoices(commands));
            AnsiConsole.Clear();
            return choice;
        }

        public static void DisplayShops(IReadOnlyList<Shop> shops)
        {
            var table = new Table();

            table.Title = new TableTitle("Registered Shops");

            table.AddColumns("id", "Shop Name", "Address");

            foreach (Shop shop in shops)
            {
                table.AddRow(shop.Id.ToString(), shop.Name, shop.Address);
            }

            AnsiConsole.Render(table);
        }

        public static void DisplayPersons(IReadOnlyList<Customer> persons)
        {
            var table = new Table();

            table.Title = new TableTitle("Registered Persons");

            table.AddColumns("id", "Person Name");

            foreach (Customer person in persons)
            {
                table.AddRow(person.Id.ToString(), person.Name);
            }

            AnsiConsole.Render(table);
        }

        public static void DisplayProducts(IReadOnlyList<Product> products)
        {
            var table = new Table();
            table.AddColumns("id", "Product Name");

            foreach (Product product in products)
            {
                table.AddRow(product.Id.ToString(), product.Name);
            }

            AnsiConsole.Render(table);
        }
    }
}