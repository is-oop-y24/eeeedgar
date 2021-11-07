using System.Collections.Generic;
using Banks.Model.Transactions;

namespace Banks.Model.Entities
{
    public class CentralBank
    {
        private int _nextbankId;
        private int _nextClientId;
        public CentralBank()
        {
            Clients = new Dictionary<int, BankClient>();
            Banks = new Dictionary<int, Bank>();
        }

        public Dictionary<int, BankClient> Clients { get; }
        public Dictionary<int, Bank> Banks { get; }

        public void MakeMoneyTransfer(MoneyTransfer moneyTransfer)
        {
            moneyTransfer.Make();
        }

        public int RegisterBank(Bank bank)
        {
            Banks.Add(_nextbankId++, bank);
            return _nextbankId - 1;
        }

        public int RegisterClient(BankClient bankClient)
        {
            Clients.Add(_nextClientId++, bankClient);
            return _nextClientId - 1;
        }
    }
}