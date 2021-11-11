using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Banks.Model.Accounts;
using Banks.Model.Entities;
using Spectre.Console;

namespace Banks.UI.EntitiesUI
{
    public class BankClientUi
    {
        public static string DisplayMenu()
        {
            var commands = new List<string>
            {
                "Display Conditions",

                "Register Debit Account",
                "Register Credit Account",
                "Register Deposit Account",

                "Display Account List",

                "Back to Central Bank",

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

        public static void DisplayClientInfo(BankClient client)
        {
            var table = new Table
            {
                Title = new TableTitle("BankClient Info"),
            };

            table.AddColumns("Id", "Name", "Surname", "Address", "Passport Data", "IsSubscribed");

            table.AddRow(client.Id.ToString(), client.Name, client.Surname, client.Address ?? "-", client.PassportData ?? "-", client.IsSubscribed.ToString());

            AnsiConsole.Write(table);
        }
    }
}