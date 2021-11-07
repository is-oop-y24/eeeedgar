using System.Collections.Generic;
using Banks.Model.Entities;
using Spectre.Console;

namespace Banks.UI.EntitiesUI
{
    public class CentralBankUi
    {
        public static string DisplayMenu()
        {
            var commands = new List<string>
            {
                "Register Bank",
                "Register Client",

                "Bank List",
                "Make Transaction",

                "Select Bank",

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

        public static void DisplayBanks(IReadOnlyDictionary<int, Bank> banks)
        {
            var table = new Table
            {
                Title = new TableTitle("Registered Banks"),
            };

            table.AddColumns("id", "Bank Name");

            foreach ((int id, Bank bank) in banks)
            {
                table.AddRow(id.ToString(), bank.Name);
            }

            AnsiConsole.Write(table);
        }

        public static void DisplayClients(IReadOnlyDictionary<int, BankClient> clients)
        {
            var table = new Table
            {
                Title = new TableTitle("Registered Clients"),
            };

            table.AddColumns("id", "Name", "Surname", "Address", "Passport Data");

            foreach ((int id, BankClient client) in clients)
            {
                table.AddRow(id.ToString(), client.Name, client.Surname, client.Address ?? "---", client.PassportData ?? "---");
            }

            AnsiConsole.Write(table);
        }
    }
}