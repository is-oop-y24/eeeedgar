using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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

        public static void DisplayAccounts(List<BankAccount> bankAccounts)
        {
            var table = new Table
            {
                Title = new TableTitle("Registered Bank Accounts"),
            };

            table.AddColumns("Id", "Type", "Balance", "Client Name", "Client Surname", "Client Address", "Client Passport Data");

            foreach (BankAccount bankAccount in bankAccounts)
            {
                table.AddRow(bankAccount.Id.ToString(), bankAccount.GetType().ToString(), bankAccount.Balance.ToString(CultureInfo.InvariantCulture), bankAccount.BankClient.Name, bankAccount.BankClient.Surname, bankAccount.BankClient.Address, bankAccount.BankClient.PassportData);
            }

            AnsiConsole.Write(table);
        }

        public static void DisplayConditions(BankingConditions bankingConditions)
        {
            AnsiConsole.WriteLine($"debit interest: {bankingConditions.DebitInterest}");
            AnsiConsole.WriteLine($"credit limit: {bankingConditions.CreditLimit}");
            AnsiConsole.WriteLine($"credit commission: {bankingConditions.CreditCommission}");
            AnsiConsole.WriteLine("deposit interest");
            for (int i = 0; i < bankingConditions.DepositInterest.ControlBalances.Count; i++)
            {
                AnsiConsole.WriteLine($"under {bankingConditions.DepositInterest.ControlBalances[i].Value} : {bankingConditions.DepositInterest.Interests[i].Value}");
            }

            AnsiConsole.WriteLine($"upper : {bankingConditions.DepositInterest.Interests.Last().Value}");
        }

        public static Guid SelectBankAccount(List<BankAccount> accounts)
        {
            var guids = new List<string>();
            foreach (BankAccount bankAccount in accounts)
            {
                guids.Add(bankAccount.Id.ToString());
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