using System.Collections.Generic;
using Shops.Entities;
using Spectre.Console;

namespace Shops.UI
{
    public class BankUI
    {
        public static string Menu()
        {
            var commands = new List<string>();
            commands.Add("Profiles");

            // commands.Add("Make a Gift");
            commands.Add("Back to Shop Manager");

            string choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Bank Menu")
                    .PageSize(10)
                    .AddChoices(commands));
            AnsiConsole.Clear();
            return choice;
        }

        public static void DisplayProfiles(Bank bank)
        {
            var table = new Table();

            table.Title = new TableTitle("Profiles");

            table.AddColumns("id", "Type", "Balance");

            foreach (BankProfile profile in bank.Profiles)
            {
                table.AddRow(profile.BankClient.Id.ToString(), profile.BankClient.GetType().ToString(), profile.Balance.ToString());
            }

            AnsiConsole.Render(table);
        }
    }
}