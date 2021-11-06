using System;
using Banks.Accounts;
using Banks.Entities;
using Banks.Transactions;

namespace Banks
{
    internal static class Program
    {
        private static void Main()
        {
            var centralBank = new CentralBank();
            var bank = new Bank();
            var client1 = new BankClient()
            {
                Name = "taylor",
                Surname = "swift",
            };
            var client2 = new BankClient()
            {
                Name = "justin",
                Surname = "biber",
            };
            var debitAccount1 = new DebitAccount(client1, 5);
            var debitAccount2 = new DebitAccount(client2, 5);

            debitAccount1.ReceiveMoney(1000);

            var moneyTransfer = new MoneyTransfer(debitAccount1, debitAccount2, 100);
            moneyTransfer.Make();

            Console.WriteLine(debitAccount1.Balance());
            Console.WriteLine(debitAccount2.Balance());
        }
    }
}
