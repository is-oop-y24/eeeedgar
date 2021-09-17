using Shops.Entities;
using Spectre.Console;

namespace Shops.UI
{
    public class BankUI
    {
        private Bank _bank;

        private BankUI(Bank bank)
        {
            _bank = bank;
        }

        public static BankUI CreateInstance(Bank bank)
        {
            return new BankUI(bank);
        }

        public void DisplayProfiles()
        {
            var table = new Table();

            table.Title = new TableTitle("Profiles");

            table.AddColumns("id", "Type", "Balance");

            foreach (BankProfile profile in _bank.Profiles)
            {
                table.AddRow(profile.BankClient.Id.ToString(), profile.BankClient.GetType().ToString(), profile.Balance.ToString());
            }

            AnsiConsole.Render(table);
        }
    }
}