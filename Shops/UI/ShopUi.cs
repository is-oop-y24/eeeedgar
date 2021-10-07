using System.Collections.Generic;
using Shops.Entities;
using Spectre.Console;

namespace Shops.UI
{
    public class ShopUi
    {
        public static string DisplayMenu(string name, string address)
        {
            var commands = new List<string>
            {
                "Stock",
                "Add Position",
                "Make Delivery",
                "Set Price",
                "Back to Shop Manager",
            };

            string choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title($"{name} on {address} Menu")
                    .PageSize(10)
                    .AddChoices(commands));
            AnsiConsole.Clear();
            return choice;
        }

        public static void DisplayStock(string shopName, string shopAddress, IReadOnlyList<StockPosition> positions)
        {
            var table = new Table();

            table.Title = new TableTitle($"Shop {shopName} on {shopAddress} Stock");

            table.AddColumns("id", "Product Name", "Amount", "Cost");

            foreach (StockPosition position in positions)
            {
                if (position != null)
                {
                    table.AddRow(position.Product.Id.ToString(), position.Product.Name, position.Amount.ToString(), position.Cost.ToString());
                }
            }

            AnsiConsole.Render(table);
        }
    }
}
