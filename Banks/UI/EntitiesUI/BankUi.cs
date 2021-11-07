using System.Collections.Generic;
using System.Globalization;
using Banks.Model.Accounts;
using Banks.Model.Entities;
using Spectre.Console;

namespace Banks.UI.EntitiesUI
{
    public class BankUi
    {
        public static string DisplayMenu()
        {
            var commands = new List<string>
            {
                "Display Accounts",

                "Register Debit Account",
                "Register Credit Account",
                "Register Deposit Account",

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

        public static void DisplayAccounts(IReadOnlyDictionary<int, IBankAccount> bankAccounts)
        {
            var table = new Table
            {
                Title = new TableTitle("Registered Bank Accounts"),
            };

            table.AddColumns("Id", "Type", "Balance");

            foreach ((int id, IBankAccount bankAccount) in bankAccounts)
            {
                table.AddRow(id.ToString(), bankAccount.StringType(), bankAccount.Balance().ToString(CultureInfo.InvariantCulture));
            }

            AnsiConsole.Write(table);
        }
    }
}