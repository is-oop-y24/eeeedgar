using System.Collections.Generic;
using System.Net;
using Shops.Entities;
using Shops.Services;
using Spectre.Console;

namespace Shops.UI
{
    public class ShopManagerUI
    {
        public static string Menu()
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

        public static void DisplayPersons(IReadOnlyList<Person> persons)
        {
            var table = new Table();

            table.Title = new TableTitle("Registered Persons");

            table.AddColumns("id", "Person Name");

            foreach (Person person in persons)
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