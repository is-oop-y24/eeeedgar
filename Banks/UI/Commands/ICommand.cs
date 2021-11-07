using Shops.Tools;

namespace Banks.UI.Commands
{
    public interface ICommand
    {
        Context Execute(Context context);
    }
}