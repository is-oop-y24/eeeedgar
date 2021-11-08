using System;
using System.Collections.Generic;
using Banks.Model.Accounts;
using Banks.Model.Entities;
using Banks.UI.Controllers;

namespace Banks
{
    internal static class Program
    {
        private static void Main()
        {
            DepositWait();
        }

        private static void Run()
        {
            var centralBank = new CentralBank();
            MainController.Run(centralBank);
        }

        private static void DepositWait()
        {
            var bankClient = new BankClient()
            {
                Name = "danya",
                Surname = "titov",
            };
            var controlBalances = new List<decimal>()
            {
                100,
                200,
            };
            var interests = new List<decimal>()
            {
                3,
                5,
                7,
            };
            var depositInterest = new DepositInterest(controlBalances, interests);
            var conditions = new BankingConditions()
            {
                DepositInterest = depositInterest,
            };
            var depositAccount = new DepositAccount(bankClient, DateTime.Now, DateTime.Now, conditions);
            const int sum = 1000;
            depositAccount.CreditFunds(sum);

            decimal computedBalance = depositAccount.Balance();
            int yearLength = DateTime.IsLeapYear(depositAccount.CreationDate.Year) ? 366 : 365;
            for (int d = 1; d <= yearLength; d++)
            {
                depositAccount.DailyRenew(depositAccount.CreationDate + TimeSpan.FromDays(d));
                Console.WriteLine($"{d} : {depositAccount.Balance()} : {depositAccount.Interest} : {depositAccount.ExpectedCharge}");
            }
        }
    }
}