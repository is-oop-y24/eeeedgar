using System;
using System.Collections.Generic;
using Banks.Model.Transactions;

namespace Banks.Model.Entities
{
    public class CentralBank
    {
        private int _nextBankId;
        private int _nextClientId;
        private int _nextTransactionId;
        public CentralBank()
        {
            Clients = new Dictionary<int, BankClient>();
            Banks = new Dictionary<int, Bank>();
            Transactions = new Dictionary<int, ITransaction>();
        }

        public Dictionary<int, BankClient> Clients { get; }
        public Dictionary<int, Bank> Banks { get; }
        public Dictionary<int, ITransaction> Transactions { get; }

        public int MakeTransaction(ITransaction transaction)
        {
            Transactions.Add(_nextTransactionId++, transaction);
            transaction.Commit();
            return _nextTransactionId - 1;
        }

        public int RegisterBank(Bank bank)
        {
            Banks.Add(_nextBankId++, bank);
            return _nextBankId - 1;
        }

        public int RegisterClient(BankClient bankClient)
        {
            Clients.Add(_nextClientId++, bankClient);
            return _nextClientId - 1;
        }

        public void CancelTransaction(ITransaction transaction)
        {
            transaction.Cancel();
        }

        public void DailyRenew(DateTime currentDate)
        {
            foreach ((int id, Bank bank) in Banks)
            {
                bank.DailyRenew(currentDate);
            }
        }
    }
}