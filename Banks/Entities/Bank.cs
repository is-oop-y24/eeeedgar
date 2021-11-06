using System.Collections.Generic;
using Banks.Accounts;

namespace Banks.Entities
{
    public class Bank
    {
        public Bank()
        {
            BankAccounts = new List<IBankAccount>();
        }

        public List<IBankAccount> BankAccounts { get; }
    }
}