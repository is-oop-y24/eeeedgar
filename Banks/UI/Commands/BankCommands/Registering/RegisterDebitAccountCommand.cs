using System;
using Banks.Model.Entities;
using Banks.UI.EntitiesUI;
using Banks.UI.Tools;

namespace Banks.UI.Commands.BankCommands.Registering
{
    public class RegisterDebitAccountCommand : ICommand
    {
        public Context Execute(Context context)
        {
            CentralBankUi.DisplayClients(context.CentralBank.Clients);
            Guid clientId = CentralBankUi.SelectClient(context.CentralBank.Clients);
            BankClient bankClient = context.CentralBank.Clients.Find(c => c.Id.Equals(clientId));
            decimal initialBalance = Clarifier.AskDecimal("initial balance");
            context.Bank.CreateDebitAccount(bankClient, initialBalance);
            return context;
        }
    }
}