using Shops.Entities;
using Shops.Tools;
using Shops.UI;
using Spectre.Console;

namespace Shops.Commands.ShopManagerCommands
{
    public class SelectCustomerCommand : ICommand
    {
        public Context Execute(Context context)
        {
            ShopManagerUi.DisplayPersons(context.ShopManager.Customers);
            int customerId = Clarifier.AskNumber("Customer Id");
            AnsiConsole.Clear();
            Customer customer = context.ShopManager.GetCustomer(customerId);
            return new Context(customer, null, context.ShopManager);
        }
    }
}