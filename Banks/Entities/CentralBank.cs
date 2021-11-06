using System.Collections.Generic;
using Banks.Transactions;

namespace Banks.Entities
{
    public class CentralBank
    {
        public CentralBank()
        {
            Banks = new List<Bank>();
        }

        public List<Bank> Banks { get; }

        public void MakeMoneyTransfer(MoneyTransfer moneyTransfer)
        {
            moneyTransfer.Make();
        }
    }
}