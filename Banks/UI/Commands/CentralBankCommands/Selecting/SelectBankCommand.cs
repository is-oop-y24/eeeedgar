using System;
using Banks.Model.Entities;
using Banks.UI.Controllers;
using Banks.UI.EntitiesUI;
using Spectre.Console;

namespace Banks.UI.Commands.CentralBankCommands.Selecting
{
    public class SelectBankCommand : ICommand
    {
        public Context Execute(Context context)
        {
            CentralBankUi.DisplayBanks(context.CentralBank.Banks);
            Guid bankId = CentralBankUi.SelectBank(context.CentralBank.Banks);
            AnsiConsole.Clear();
            Bank bank = context.CentralBank.Banks.Find(b => b.Id.Equals(bankId));
            return new Context(context.CentralBank, bank);
        }
    }
}