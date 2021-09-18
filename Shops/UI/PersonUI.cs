using System.Collections.Generic;
using Shops.Entities;
using Shops.Services;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Shops.UI
{
    public class PersonUI
    {
        public static void Menu(Person person)
        {
            var commands = new List<string>();
            commands.Add("Wishlist");
            commands.Add("Add Item to Wishlist");
            commands.Add("Buy (!)");
            commands.Add("Back to Shop Manager");

            string choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title($"Person {person.Id} {person.Name}")
                    .PageSize(10)
                    .AddChoices(commands));
            AnsiConsole.Clear();

            switch (choice)
            {
                case "Wishlist":
                {
                    DisplayWishList(person);
                    AnsiConsole.Confirm("type to exit");
                    AnsiConsole.Clear();
                    Menu(person);
                    break;
                }

                case "Add Item to Wishlist":
                {
                    person.AddItemToWishList(Clarifier.AskNumber("product id"), Clarifier.AskNumber("amount"));
                    AnsiConsole.Clear();
                    Menu(person);
                    break;
                }

                case "Back to Shop Manager":
                {
                    ShopManagerUI.Menu(person.ShopManager);
                    break;
                }
            }
        }

        private static void DisplayWishList(Person person)
        {
            var table = new Table();

            table.Title = new TableTitle($"Person {person.Id} {person.Name} WishList");

            table.AddColumns("id", "Product Name", "Amount");

            foreach (Purchase purchase in person.WishList)
            {
                table.AddRow(purchase.Product.Id.ToString(), purchase.Product.Name, purchase.Amount.ToString());
            }

            AnsiConsole.Render(table);
        }
    }
}