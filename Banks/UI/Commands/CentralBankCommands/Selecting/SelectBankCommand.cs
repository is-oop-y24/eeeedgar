using Banks.Model.Entities;
using Banks.UI.EntitiesUI;
using Banks.UI.Tools;
using Spectre.Console;

namespace Banks.UI.Commands.CentralBankCommands.Selecting
{
    public class SelectBankCommand : ICommand
    {
        public Context Execute(Context context)
        {
            CentralBankUi.DisplayBanks(context.CentralBank.Banks);
            int bankId = (int)Clarifier.AskDecimal("Bank Id");
            AnsiConsole.Clear();
            Bank bank = context.CentralBank.Banks[bankId];
            return new Context(context.CentralBank, bank);
        }
    }
}