using Banks.Model.Entities;
using Banks.UI.Controllers;
using Banks.UI.EntitiesUI;

namespace Banks.UI.Commands.CentralBankCommands.Registering
{
    public class RegisterClientCommand : ICommand
    {
        public Context Execute(Context context)
        {
            string name = Clarifier.AskString("client name");
            string surname = Clarifier.AskString("client surname");
            var bankClient = BankClient.CreateInstance(name, surname);
            context.CentralBank.RegisterClient(bankClient);
            return context;
        }
    }
}