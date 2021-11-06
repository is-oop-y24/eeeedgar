using System;
using System.Collections.Generic;
using System.Linq;

namespace Banks.Accounts
{
    public class DepositInterest
    {
        public DepositInterest(List<decimal> controlBalances, List<decimal> interests)
        {
            ControlBalances = controlBalances;
            Interests = interests;
        }

        public List<decimal> ControlBalances { get; }
        public List<decimal> Interests { get; }

        public decimal Interest(decimal balance)
        {
            int controlBalanceNumber = 0;
            while (controlBalanceNumber < ControlBalances.Count && ControlBalances[controlBalanceNumber] < balance)
            {
                Console.WriteLine($"dep int: {ControlBalances[controlBalanceNumber]})");
                controlBalanceNumber++;
            }

            Console.WriteLine($"ans: {(ControlBalances.Last() >= balance ? Interests[controlBalanceNumber] : Interests.Last())}");
            return ControlBalances.Last() >= balance ? Interests[controlBalanceNumber] : Interests.Last();
        }
    }
}