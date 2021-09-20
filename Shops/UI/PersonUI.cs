using System.Collections.Generic;
using Shops.Entities;
using Shops.Services;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Shops.UI
{
    public class PersonUI
    {
        public static string Menu(int id, string name)
        {
            var commands = new List<string>();
            commands.Add("Money");

            commands.Add("Buy");
            commands.Add("Back to Shop Manager");

            string choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title($"Person {id} {name}")
                    .PageSize(10)
                    .AddChoices(commands));
            AnsiConsole.Clear();

            return choice;
        }

        public static void DisplayWishList(int id, string name, IReadOnlyList<Purchase> wishlist)
        {
            var table = new Table();

            table.Title = new TableTitle($"Person {id} {name} WishList");

            table.AddColumns("id", "Product Name", "Amount");

            foreach (Purchase purchase in wishlist)
            {
                table.AddRow(purchase.Product.Id.ToString(), purchase.Product.Name, purchase.Amount.ToString());
            }

            AnsiConsole.Render(table);
        }
    }
}