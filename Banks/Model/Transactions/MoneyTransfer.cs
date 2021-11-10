using System;
using Banks.Model.Tools;

namespace Banks.Model.Transactions
{
    public class MoneyTransfer : Transaction
    {
        public override Type Type => GetType();

        public override void Commit()
        {
            if (IsCommitted)
                throw new BanksException("retry to commit a transaction");
            if (!Sender.IsConfirmed() && Money > Sender.BankingConditions.DoubtfulAccountLimit)
                throw new BanksException("exceeding the limit for doubtful accounts");
            Sender.DeductFunds(Money);
            Receiver.CreditFunds(Money);
            IsCommitted = true;
        }

        public override void Cancel()
        {
            if (IsCanceled || !IsCommitted)
                throw new BanksException("retry to cancel a transaction");
            Receiver.DeductFunds(Money);
            Sender.CreditFunds(Money);
            IsCanceled = true;
        }
    }
}