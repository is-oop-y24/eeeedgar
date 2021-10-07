using Shops.Tools;
using Shops.UI;
using Spectre.Console;

namespace Shops.Commands.ShopManagerCommands
{
    public class RegisterCustomerCommand : ICommand
    {
        public Context Execute(Context context)
        {
            string customerName = Clarifier.AskString("Customer Name");
            int customerBalance = Clarifier.AskNumber("Customer Balance");
            AnsiConsole.Clear();
            context.ShopManager.RegisterCustomer(customerName, customerBalance);
            return new Context(null, null, context.ShopManager);
        }
    }
}