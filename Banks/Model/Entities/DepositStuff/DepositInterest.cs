using System;
using System.Collections.Generic;
using System.Linq;

namespace Banks.Model.Entities.DepositStuff
{
    public class DepositInterest
    {
        public Guid Id { get; set; }
        public List<DepositControlBalance> ControlBalances { get; init; }
        public List<DepositControlInterest> Interests { get; init; }

        public decimal Count(decimal balance)
        {
            int controlBalanceNumber = 0;
            while (controlBalanceNumber < ControlBalances.Count && ControlBalances[controlBalanceNumber].Value < balance)
            {
                controlBalanceNumber++;
            }

            return ControlBalances.Last().Value >= balance ? Interests[controlBalanceNumber].Value : Interests.Last().Value;
        }
    }
}