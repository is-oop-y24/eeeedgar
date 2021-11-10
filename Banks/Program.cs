using System;
using System.Linq;
using Banks.Model.Entities;
using Banks.Model.Transactions;
using Banks.Repository.EF;
using Banks.UI.Controllers;

namespace Banks
{
    internal static class Program
    {
        private static void Main()
        {
            Run();
        }

        private static void Run()
        {
            var centralBank = new CentralBank();
            MainController.Run(centralBank);
            Console.WriteLine(centralBank.Banks);
            SaveAndDisplayDataBase(centralBank);
        }

        private static void SaveAndDisplayDataBase(CentralBank centralBank)
        {
            using var db = new ApplicationContext();
            db.CentralBanks.Add(centralBank);

            db.SaveChanges();

            CentralBank c = db.CentralBanks.First();
            Console.WriteLine("------- db: banks -------");
            foreach (Bank bank in c.Banks)
            {
                Console.WriteLine($"{bank.Id} | {bank.Name}");
            }

            Console.WriteLine("------- db: clients -------");
            foreach (BankClient client in c.Clients)
            {
                Console.WriteLine($"{client.Id} | {client.Name}");
            }

            Console.WriteLine("------- db: transactions -------");
            foreach (Transaction transaction in c.Transactions)
            {
                Console.WriteLine($"{transaction.Id} | {transaction.Type}");
            }
        }
    }
}