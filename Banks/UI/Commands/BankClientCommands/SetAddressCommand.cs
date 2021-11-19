using Banks.UI.Controllers;
using Banks.UI.EntitiesUI;

namespace Banks.UI.Commands.BankClientCommands
{
    public class SetAddressCommand : ICommand
    {
        public Context Execute(Context context)
        {
            string address = Clarifier.AskString("address");
            context.BankClient.SetAddress(address);
            return context;
        }
    }
}