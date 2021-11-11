using System;
using Banks.Model.Entities;
using Banks.UI.Controllers;
using Banks.UI.EntitiesUI;
using Spectre.Console;

namespace Banks.UI.Commands.CentralBankCommands.Selecting
{
    public class SelectClientCommand : ICommand
    {
        public Context Execute(Context context)
        {
            CentralBankUi.DisplayClients(context.CentralBank.Clients);
            Guid clientId = CentralBankUi.SelectClient(context.CentralBank.Clients);
            AnsiConsole.Clear();
            BankClient client = context.CentralBank.Clients.Find(c => c.Id.Equals(clientId));
            return new Context(context.CentralBank, context.Bank, client);
        }
    }
}