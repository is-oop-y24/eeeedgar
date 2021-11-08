using Banks.Model.Entities;

namespace Banks.UI.Tools
{
    public class Context
    {
        public Context(CentralBank centralBank = null, Bank bank = null)
        {
            CentralBank = centralBank;
            Bank = bank;
        }

        public string Choice { get; }
        public CentralBank CentralBank { get; }
        public Bank Bank { get; }
    }
}