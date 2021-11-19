using System;
using Banks.Model.Accounts;
using Banks.Model.Entities;
using Banks.Model.Transactions;
using Banks.UI.Commands.CentralBankCommands.Selecting;
using Banks.UI.Controllers;
using Banks.UI.EntitiesUI;
using Spectre.Console;

namespace Banks.UI.Commands.CentralBankCommands.MakingTransactions
{
    public class MakeMoneyTransferCommand : ICommand
    {
        public Context Execute(Context context)
        {
            Bank senderBank = new SelectBankCommand().Execute(context).Bank;
            BankUi.DisplayAccounts(senderBank.BankAccounts);
            AnsiConsole.WriteLine("sender");
            Guid senderAccountId = BankUi.SelectBankAccount(senderBank.BankAccounts);
            BankAccount senderAccount = senderBank.BankAccounts.Find(a => a.Id.Equals(senderAccountId));

            Bank receiverBank = new SelectBankCommand().Execute(context).Bank;
            BankUi.DisplayAccounts(receiverBank.BankAccounts);
            AnsiConsole.WriteLine("receiver");
            Guid receiverAccountId = BankUi.SelectBankAccount(receiverBank.BankAccounts);
            BankAccount receiverAccount = receiverBank.BankAccounts.Find(a => a.Id.Equals(receiverAccountId));

            decimal money = Clarifier.AskDecimal("money transfer value");
            var moneyTransfer = new MoneyTransfer
            {
                Id = Guid.NewGuid(),
                Sender = senderAccount,
                Receiver = receiverAccount,
                Money = money,
            };
            context.CentralBank.MakeTransaction(moneyTransfer);

            return context;
        }
    }
}