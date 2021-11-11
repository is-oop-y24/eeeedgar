using System;
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

                "Display Bank List",
                "Display Client List",

                "Make Money Transfer",
                "Make Account Replenishment",
                "Make Funds Withdrawal",

                "Select Bank",
                "Select Client",

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

        public static void DisplayBanks(List<Bank> banks)
        {
            var table = new Table
            {
                Title = new TableTitle("Registered Banks"),
            };

            table.AddColumns("id", "Bank Name");

            foreach (Bank bank in banks)
            {
                table.AddRow(bank.Id.ToString(), bank.Name);
            }

            AnsiConsole.Write(table);
        }

        public static void DisplayClients(List<BankClient> clients)
        {
            var table = new Table
            {
                Title = new TableTitle("Registered Clients"),
            };

            table.AddColumns("id", "Name", "Surname", "Address", "Passport Data");

            foreach (BankClient client in clients)
            {
                table.AddRow(client.Id.ToString(), client.Name, client.Surname, client.Address ?? "---", client.PassportData ?? "---");
            }

            AnsiConsole.Write(table);
        }

        public static Guid SelectBank(List<Bank> banks)
        {
            var guids = new List<string>();
            foreach (Bank bank in banks)
            {
                guids.Add(bank.Id.ToString());
            }

            string choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Select Bank")
                    .PageSize(10)
                    .AddChoices(guids));
            AnsiConsole.Clear();
            return Guid.Parse((ReadOnlySpan<char>)choice);
        }

        public static Guid SelectClient(List<BankClient> clients)
        {
            var guids = new List<string>();
            foreach (BankClient client in clients)
            {
                guids.Add(client.Id.ToString());
            }

            string choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Select Client")
                    .PageSize(10)
                    .AddChoices(guids));
            AnsiConsole.Clear();
            return Guid.Parse((ReadOnlySpan<char>)choice);
        }
    }
}