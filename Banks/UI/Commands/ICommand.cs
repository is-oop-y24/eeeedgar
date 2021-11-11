using Banks.UI.Controllers;

namespace Banks.UI.Commands
{
    public interface ICommand
    {
        Context Execute(Context context);
    }
}