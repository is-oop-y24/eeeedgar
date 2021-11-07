using Banks.Model.Entities;

namespace Shops.Tools
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