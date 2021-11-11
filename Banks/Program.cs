using System;
using System.Collections.Generic;
using System.Linq;
using Banks.Model.Entities;
using Banks.Model.Entities.DepositStuff;
using Banks.Model.Transactions;
using Banks.Repository.EF;
using Banks.UI.Controllers;
using Microsoft.EntityFrameworkCore;

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
            // var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            // DbContextOptions<ApplicationContext> options = optionsBuilder
            //     .UseSqlServer(@"Server=localhost\\SQLEXPRESS;Database=master;Trusted_Connection=True;")
            //     .Options;
            // using var db = new ApplicationContext(options);
            using var db = new ApplicationContext();
            var centralBank = new CentralBank();
            ClearDataBase(db);

            // MainController.Run(centralBank);
            centralBank.RegisterBank(
                Bank.CreateInstance("sber")
                    .SetCreditCommission(200)
                    .SetCreditLimit(15000)
                    .SetDebitInterest(3)
                    .SetDepositInterest(new DepositInterest())
                    .SetDoubtfulAccountLimit(5000));
            centralBank.RegisterClient(
                BankClient.CreateInstance("ve", "ka")
                    .SetAddress("address")
                    .SetPassportData("228 1337 666")
                    .Subscribe());
            centralBank.Banks.First().CreateDebitAccount(centralBank.Clients.First(), 0);
            var accountReplenishment = new AccountReplenishment()
            {
                Id = Guid.NewGuid(),
                Money = 10,
                Receiver = centralBank.Banks.First().BankAccounts.First(),
            };
            centralBank.MakeTransaction(accountReplenishment);
            db.CentralBanks.Add(centralBank);
            DisplayData(db);
        }

        private static void ClearDataBase(ApplicationContext db)
        {
            foreach (CentralBank centralBank in db.CentralBanks)
            {
                db.Remove(centralBank);
            }
        }

        private static void DisplayData(ApplicationContext db)
        {
            var centralBanks = db.CentralBanks.ToList();
            Console.WriteLine("------- db: centralBanks -------");
            foreach (CentralBank cb in centralBanks)
            {
                Console.WriteLine("------- db: banks -------");
                foreach (Bank bank in cb.Banks)
                {
                    Console.WriteLine($"{bank.Id} | {bank.Name}");
                }

                Console.WriteLine("------- db: clients -------");
                foreach (BankClient client in cb.Clients)
                {
                    Console.WriteLine($"{client.Id} | {client.Name}");
                }

                Console.WriteLine("------- db: transactions -------");
                foreach (Transaction transaction in cb.Transactions)
                {
                    Console.WriteLine($"{transaction.Id} | {transaction.Type}");
                }
            }
        }
    }
}