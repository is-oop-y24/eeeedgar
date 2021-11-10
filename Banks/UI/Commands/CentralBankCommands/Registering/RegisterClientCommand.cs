using System;
using Banks.Model.Entities;
using Banks.UI.EntitiesUI;
using Banks.UI.Tools;

namespace Banks.UI.Commands.CentralBankCommands.Registering
{
    public class RegisterClientCommand : ICommand
    {
        public Context Execute(Context context)
        {
            string name = Clarifier.AskString("client name");
            string surname = Clarifier.AskString("client surname");
            string address = Clarifier.AskString("client address");
            string passportData = Clarifier.AskString("client passport data");
            var bankClient = new BankClient
            {
                Id = Guid.NewGuid(),
                Name = name,
                Surname = surname,
                Address = address,
                PassportData = passportData,
            };
            context.CentralBank.RegisterClient(bankClient);
            return context;
        }
    }
}