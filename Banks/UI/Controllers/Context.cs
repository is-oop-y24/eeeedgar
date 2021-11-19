using Banks.Model.Entities;

namespace Banks.UI.Controllers
{
    public class Context
    {
        public Context(CentralBank centralBank = null, Bank bank = null, BankClient bankClient = null)
        {
            CentralBank = centralBank;
            Bank = bank;
            BankClient = bankClient;
        }

        public string Choice { get; }
        public CentralBank CentralBank { get; }
        public Bank Bank { get; }
        public BankClient BankClient { get; }
    }
}