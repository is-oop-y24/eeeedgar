using System;
using Banks.Model.Accounts;
using Banks.Model.Entities;
using Banks.Model.Transactions;
using Banks.UI.Commands.CentralBankCommands.Selecting;
using Banks.UI.Controllers;
using Banks.UI.EntitiesUI;

namespace Banks.UI.Commands.CentralBankCommands.MakingTransactions
{
    public class MakeAccountReplenishmentCommand : ICommand
    {
        public Context Execute(Context context)
        {
            Bank accountBank = new SelectBankCommand().Execute(context).Bank;
            BankUi.DisplayAccounts(accountBank.BankAccounts);
            Guid bankAccountId = BankUi.SelectBankAccount(accountBank.BankAccounts);
            BankAccount account = accountBank.BankAccounts.Find(a => a.Id.Equals(bankAccountId));

            decimal money = Clarifier.AskDecimal("money transfer value");
            var accountReplenishment = new AccountReplenishment
            {
                Id = Guid.NewGuid(),
                Receiver = account,
                Money = money,
            };
            context.CentralBank.MakeTransaction(accountReplenishment);

            return context;
        }
    }
}