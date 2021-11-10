using System;
using Banks.Model.Tools;

namespace Banks.Model.Transactions
{
    public class AccountReplenishment : Transaction
    {
        public override Type Type => GetType();

        public override void Commit()
        {
            if (IsCommitted)
                throw new BanksException("retry to commit a transaction");
            Receiver.CreditFunds(Money);
            IsCommitted = true;
        }

        public override void Cancel()
        {
            if (IsCanceled || !IsCommitted)
                throw new BanksException("retry to cancel a transaction");
            Receiver.DeductFunds(Money);
            IsCanceled = true;
        }
    }
}