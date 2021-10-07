using Shops.Entities;
using Shops.Tools;
using Shops.UI;
using Spectre.Console;

namespace Shops.Commands.ShopManagerCommands
{
    public class RegisterProductCommand : ICommand
    {
        public Context Execute(Context context)
        {
            string productName = Clarifier.AskString("Product Name");
            AnsiConsole.Clear();
            GlobalProductBase.RegisterProduct(productName);
            return new Context(null, null, context.ShopManager);
        }
    }
}