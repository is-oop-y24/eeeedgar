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
            int clientId = (int)Clarifier.AskDecimal("client id");
            BankClient bankClient = context.CentralBank.Clients[clientId];
            context.Bank.CreateDebitAccount(bankClient);
            return context;
        }
    }
}