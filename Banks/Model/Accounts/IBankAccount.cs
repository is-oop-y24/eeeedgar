using System;
using Banks.Model.Entities;

namespace Banks.Model.Accounts
{
    public interface IBankAccount
    {
        decimal Balance();

        void DeductFunds(decimal money);

        void DailyRenew(DateTime currentDate);

        void CreditFunds(decimal money);
        string StringType();
        bool IsConfirmed();
        BankClient BankClient();
        BankingConditions BankingConditions();
        void NotifyClient();
    }
}