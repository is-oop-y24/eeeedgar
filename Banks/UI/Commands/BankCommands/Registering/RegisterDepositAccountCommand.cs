using System;
using Banks.Model.Entities;
using Banks.UI.EntitiesUI;
using Banks.UI.Tools;

namespace Banks.UI.Commands.BankCommands.Registering
{
    public class RegisterDepositAccountCommand : ICommand
    {
        public Context Execute(Context context)
        {
            CentralBankUi.DisplayClients(context.CentralBank.Clients);
            Guid clientId = CentralBankUi.SelectClient(context.CentralBank.Clients);
            BankClient bankClient = context.CentralBank.Clients.Find(c => c.Id.Equals(clientId));
            decimal startBalance = Clarifier.AskDecimal("start balance");
            context.Bank.CreateDepositAccount(bankClient, startBalance);
            return context;
        }
    }
}