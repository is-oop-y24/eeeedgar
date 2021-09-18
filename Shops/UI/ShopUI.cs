using System.Collections.Generic;
using Shops.Entities;
using Shops.Services;
using Spectre.Console;

namespace Shops.UI
{
    public class ShopUI
    {
        public static void Menu(Shop shop)
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
                    .Title($"{shop.Name} on {shop.Address} Menu")
                    .PageSize(10)
                    .AddChoices(commands));
            AnsiConsole.Clear();

            switch (choice)
            {
                case "Stock":
                {
                    DisplayStock(shop);
                    AnsiConsole.Confirm("type to exit");
                    AnsiConsole.Clear();
                    Menu(shop);
                    break;
                }

                case "Show Permitted Products":
                {
                    DisplayPermittedProducts(shop);
                    AnsiConsole.Confirm("type to exit");
                    AnsiConsole.Clear();
                    Menu(shop);
                    break;
                }

                case "Add Position":
                {
                    shop.AddPosition(Clarifier.AskNumber("product id"));
                    AnsiConsole.Clear();
                    Menu(shop);
                    break;
                }

                case "Make Delivery":
                {
                    AnsiConsole.Clear();
                    shop.AddProducts(Clarifier.AskNumber("product id"), Clarifier.AskNumber("amount"));
                    AnsiConsole.Clear();
                    Menu(shop);
                    break;
                }

                case "Set Price":
                {
                    shop.SetProductPrice(shop.GetPosition(Clarifier.AskNumber("product id")).Product, Clarifier.AskNumber("new product price"));
                    AnsiConsole.Clear();
                    Menu(shop);
                    break;
                }

                case "Back to Shop Manager":
                {
                    AnsiConsole.Clear();
                    ShopManagerUI.Menu(shop.ShopManager);
                    break;
                }
            }
        }

        private static void DisplayStock(Shop shop)
        {
            var table = new Table();

            table.Title = new TableTitle($"Shop {shop.Id} {shop.Name} Stock");

            table.AddColumns("id", "Product Name", "Amount", "Cost");

            foreach (Position position in shop.Stock)
            {
                if (position != null)
                {
                    table.AddRow(position.Product.Id.ToString(), position.Product.Name, position.Amount.ToString(), position.Cost.ToString());
                }
            }

            AnsiConsole.Render(table);
        }

        private static void DisplayPermittedProducts(Shop shop)
        {
            var table = new Table();

            table.Title = new TableTitle($"Shop {shop.Id} {shop.Name} Stock");

            table.AddColumns("id", "Product Name");

            foreach (Product product in shop.PermittedProducts)
            {
                table.AddRow(product.Id.ToString(), product.Name);
            }

            AnsiConsole.Render(table);
        }
    }
}
