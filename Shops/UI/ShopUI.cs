using System.Collections.Generic;
using Shops.Entities;
using Shops.Services;
using Spectre.Console;

namespace Shops.UI
{
    public class ShopUI
    {
        public static string Menu(string name, string address)
        {
            var commands = new List<string>();
            commands.Add("Stock");
            commands.Add("Show Permitted Products");
            commands.Add("Add Position");
            commands.Add("Make Delivery");
            commands.Add("Set Price");
            commands.Add("Back to Shop Manager");

            string choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title($"{name} on {address} Menu")
                    .PageSize(10)
                    .AddChoices(commands));
            AnsiConsole.Clear();
            return choice;
        }

        public static void DisplayStock(int id, string name, IReadOnlyList<Position> positions)
        {
            var table = new Table();

            table.Title = new TableTitle($"Shop {id} {name} Stock");

            table.AddColumns("id", "Product Name", "Amount", "Cost");

            foreach (Position position in positions)
            {
                if (position != null)
                {
                    table.AddRow(position.Product.Id.ToString(), position.Product.Name, position.Amount.ToString(), position.Cost.ToString());
                }
            }

            AnsiConsole.Render(table);
        }

        public static void DisplayPermittedProducts(int id, string name, IReadOnlyList<Product> products)
        {
            var table = new Table();

            table.Title = new TableTitle($"Shop {id} {name} Stock");

            table.AddColumns("id", "Product Name");

            foreach (Product product in products)
            {
                table.AddRow(product.Id.ToString(), product.Name);
            }

            AnsiConsole.Render(table);
        }
    }
}
