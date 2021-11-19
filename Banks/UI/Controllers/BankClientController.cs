using Banks.Model.Tools;
using Banks.UI.Commands.BankClientCommands;
using Banks.UI.EntitiesUI;

namespace Banks.UI.Controllers
{
    public static class BankClientController
    {
        public static Context CheckBankClientUiChoice(Context context)
        {
            BankClientUi.DisplayClientInfo(context.BankClient);
            string choice = BankClientUi.DisplayMenu();
            return choice switch
            {
                "Set Address" => new SetAddressCommand().Execute(context),
                "Set Passport Data" => new SetPassportDataCommand().Execute(context),
                "Subscribe" => new SetSubscriptionCommand(true).Execute(context),
                "Unsubscribe" => new SetSubscriptionCommand(false).Execute(context),

                "Back to Central Bank" => new Context(context.CentralBank, null),

                "Exit" => new Context(null, null),
                _ => throw new BanksException("input error")
            };
        }
    }
}