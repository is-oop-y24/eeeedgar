using Banks.Model.Tools;
using Banks.UI.Commands.BankCommands.Displaying;
using Banks.UI.Commands.BankCommands.Registering;
using Banks.UI.EntitiesUI;
using Banks.UI.Tools;

namespace Banks.UI.Controllers
{
    public static class BankController
    {
        public static Context CheckBankUiChoice(Context context)
        {
            string choice = BankUi.DisplayMenu();
            return choice switch
            {
                "Display Conditions" => new DisplayBankingConditionsCommand().Execute(context),
                "Register Debit Account" => new RegisterDebitAccountCommand().Execute(context),
                "Register Deposit Account" => new RegisterDepositAccountCommand().Execute(context),
                "Register Credit Account" => new RegisterCreditAccountCommand().Execute(context),

                "Display Account List" => new DisplayAccountsCommand().Execute(context),

                "Back to Central Bank" => new Context(context.CentralBank, null),

                "Exit" => new Context(null, null),
                _ => throw new BanksException("input error")
            };
        }
    }
}