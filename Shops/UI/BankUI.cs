using System.Collections.Generic;
using Shops.Entities;
using Spectre.Console;

namespace Shops.UI
{
    public class BankUI
    {
        public static void Menu(Bank bank)
        {
            var commands = new List<string>();
            commands.Add("Profiles");
            commands.Add("Make a Gift");
            commands.Add("Back to Shop Manager");

            string choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Bank Menu")
                    .PageSize(10)
                    .AddChoices(commands));
            AnsiConsole.Clear();

            switch (choice)
            {
                case "Profiles":
                {
                    DisplayProfiles(bank);
                    AnsiConsole.Confirm("type to exit");
                    AnsiConsole.Clear();
                    Menu(bank);
                    break;
                }

                case "Make a Gift":
                {
                    bank.GiveMoney(Clarifier.AskNumber("person id"), Clarifier.AskNumber("money amount"));
                    AnsiConsole.Clear();
                    Menu(bank);
                    break;
                }

                case "Back to Shop Manager":
                {
                    AnsiConsole.Clear();
                    ShopManagerUI.Menu(bank.ShopManager);
                    break;
                }
            }
        }

        private static void DisplayProfiles(Bank bank)
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