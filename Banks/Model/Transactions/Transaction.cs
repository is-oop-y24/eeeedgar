using System;
using Banks.Model.Accounts;

namespace Banks.Model.Transactions
{
    public abstract class Transaction
    {
        public Guid Id { get; set; }
        public abstract Type Type { get; }
        public BankAccount Sender { get; set; }
        public BankAccount Receiver { get; set; }
        public decimal Money { get; set; }
        public bool IsCommitted { get; set; }
        public bool IsCanceled { get; set; }
        public abstract void Commit();
        public abstract void Cancel();
    }
}