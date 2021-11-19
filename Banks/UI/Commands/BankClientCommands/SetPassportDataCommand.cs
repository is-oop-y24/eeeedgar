using Banks.UI.Controllers;
using Banks.UI.EntitiesUI;

namespace Banks.UI.Commands.BankClientCommands
{
    public class SetPassportDataCommand : ICommand
    {
        public Context Execute(Context context)
        {
            string passportData = Clarifier.AskString("passport data");
            context.BankClient.SetPassportData(passportData);
            return context;
        }
    }
}