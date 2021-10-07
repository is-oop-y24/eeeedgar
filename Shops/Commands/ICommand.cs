using Shops.Tools;

namespace Shops.Commands
{
    public interface ICommand
    {
        Context Execute(Context context);
    }
}