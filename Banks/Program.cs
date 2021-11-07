using System;
using Banks.Model.Accounts;
using Banks.Model.Entities;
using Banks.Model.Transactions;
using Banks.UI.EntitiesUI;

namespace Banks
{
    internal static class Program
    {
        private static void Main()
        {
            DisplayAccounts();
        }

        private static void DisplayBanks()
        {
            var centralBank = new CentralBank();
            var bank1 = new Bank
            {
                Name = "sbebrank",
            };
            var bank2 = new Bank
            {
                Name = "tonkoff",
            };
            var bank3 = new Bank
            {
                Name = "rifizen",
            };
            var bank4 = new Bank
            {
                Name = "bspb",
            };
            centralBank.RegisterBank(bank1);
            centralBank.RegisterBank(bank2);
            centralBank.RegisterBank(bank3);
            centralBank.RegisterBank(bank4);
            CentralBankUi.DisplayBanks(centralBank.Banks);
        }

        private static void DisplayClients()
        {
            var centralBank = new CentralBank();
            var bankClient1 = new BankClient()
            {
                Name = "taylor",
                Surname = "swift",
            };
            var bankClient2 = new BankClient()
            {
                Name = "hello",
                Surname = "world",
                Address = "pepega",
                PassportData = "4020 228877",
            };
            centralBank.RegisterClient(bankClient1);
            centralBank.RegisterClient(bankClient2);
            CentralBankUi.DisplayClients(centralBank.Clients);
        }

        private static void DisplayAccounts()
        {
            var centralBank = new CentralBank();
            var bankClient1 = new BankClient()
            {
                Name = "taylor",
                Surname = "swift",
            };
            var bankClient2 = new BankClient()
            {
                Name = "hello",
                Surname = "world",
                Address = "pepega",
                PassportData = "4020 228877",
            };
            centralBank.RegisterClient(bankClient1);
            centralBank.RegisterClient(bankClient2);
            var bank = new Bank
            {
                Name = "sbebrank",
            };
            CreditAccount creditAccount = bank.CreateCreditAccount(bankClient1);
            creditAccount.ReceiveMoney(100);
            bank.CreateCreditAccount(bankClient2);
            bank.CreateDepositAccount(bankClient1);
            bank.CreateDebitAccount(bankClient1);
            BankUi.DisplayAccounts(bank.BankAccounts);
        }
    }
}
