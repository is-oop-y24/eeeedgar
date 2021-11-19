using Banks.Model.Tools;
using Banks.UI.Commands.CentralBankCommands.Displaying;
using Banks.UI.Commands.CentralBankCommands.MakingTransactions;
using Banks.UI.Commands.CentralBankCommands.Registering;
using Banks.UI.Commands.CentralBankCommands.Selecting;
using Banks.UI.EntitiesUI;

namespace Banks.UI.Controllers
{
    public static class CentralBankController
    {
        public static Context CheckCentralBankUiChoice(Context context)
        {
            string choice = CentralBankUi.DisplayMenu();
            return choice switch
            {
                "Register Bank" => new RegisterBankCommand().Execute(context),
                "Register Client" => new RegisterClientCommand().Execute(context),

                "Display Bank List" => new DisplayBanksCommand().Execute(context),
                "Display Client List" => new DisplayClientsCommand().Execute(context),

                "Make Money Transfer" => new MakeMoneyTransferCommand().Execute(context),
                "Make Account Replenishment" => new MakeAccountReplenishmentCommand().Execute(context),
                "Make Funds Withdrawal" => new MakeFundsWithdrawalCommand().Execute(context),

                "Select Bank" => new SelectBankCommand().Execute(context),
                "Select Client" => new SelectClientCommand().Execute(context),

                "Exit" => new Context(null, null),
                _ => throw new BanksException("input error")
            };
        }
    }
}