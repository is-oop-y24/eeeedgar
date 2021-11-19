using System;
using System.Collections.Generic;
using Banks.Model.Transactions;

namespace Banks.Model.Entities
{
    public class CentralBank
    {
        public CentralBank()
        {
            CurrentDate = DateTime.Today;
            Clients = new List<BankClient>();
            Banks = new List<Bank>();
            Transactions = new List<Transaction>();
        }

        public Guid Id { get; set; }
        public List<BankClient> Clients { get; }
        public List<Bank> Banks { get; }
        public List<Transaction> Transactions { get; }
        public DateTime CurrentDate { get; private set; }

        public void MakeTransaction(Transaction transaction)
        {
            Transactions.Add(transaction);
            transaction.Commit();
        }

        public void RegisterBank(Bank bank)
        {
            Banks.Add(bank);
        }

        public void RegisterClient(BankClient bankClient)
        {
            Clients.Add(bankClient);
        }

        public void CancelTransaction(Transaction transaction)
        {
            transaction.Cancel();
        }

        public void DailyRenew()
        {
            foreach (Bank bank in Banks)
            {
                bank.DailyRenew(CurrentDate);
            }

            CurrentDate += TimeSpan.FromDays(1);
        }
    }
}