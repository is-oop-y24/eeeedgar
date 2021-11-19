using Banks.Model.Entities;

namespace Banks.UI.Controllers
{
    public static class MainController
    {
        public static void Run(CentralBank centralBank)
        {
            var context = new Context(centralBank, null);
            while (context.CentralBank != null)
            {
                context = RunControllers(context);
            }
        }

        private static Context RunControllers(Context context)
        {
            if (context.BankClient != null)
                return BankClientController.CheckBankClientUiChoice(context);
            if (context.Bank != null)
                return BankController.CheckBankUiChoice(context);
            return CentralBankController.CheckCentralBankUiChoice(context);
        }
    }
}