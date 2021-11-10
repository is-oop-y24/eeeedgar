using System;
using System.Collections.Generic;
using Banks.Model.Entities;
using Banks.Model.Entities.DepositStuff;
using Banks.UI.EntitiesUI;
using Banks.UI.Tools;

namespace Banks.UI.Commands.CentralBankCommands.Registering
{
    public class RegisterBankCommand : ICommand
    {
        public Context Execute(Context context)
        {
            string bankName = Clarifier.AskString("bank name");
            decimal creditLimit = Clarifier.AskDecimal("credit limit");
            decimal creditCommission = Clarifier.AskDecimal("credit commission");
            decimal debitInterest = Clarifier.AskDecimal("debit interest");
            int depositControlPointsNumber = (int)Clarifier.AskDecimal("deposit control points number");
            var depositControlPointsDecimal = new List<decimal>();
            var depositInterestsDecimal = new List<decimal>();
            for (int i = 0; i < depositControlPointsNumber; i++)
            {
                decimal depositControlSum = Clarifier.AskDecimal("deposit control sum");
                depositControlPointsDecimal.Add(depositControlSum);
                decimal depositInterestUnderControlSum = Clarifier.AskDecimal("deposit interest under control sum");
                depositInterestsDecimal.Add(depositInterestUnderControlSum);
            }

            {
                decimal depositInterestUnlimited = Clarifier.AskDecimal("deposit interest for unlimited sum");
                depositInterestsDecimal.Add(depositInterestUnlimited);
            }

            var depositControlBalances = new List<DepositControlBalance>();
            foreach (decimal depositControlPoint in depositControlPointsDecimal)
            {
                depositControlBalances.Add(new DepositControlBalance { Value = depositControlPoint });
            }

            var depositControlInterests = new List<DepositControlInterest>();
            foreach (decimal interestDecimal in depositInterestsDecimal)
            {
                depositControlInterests.Add(new DepositControlInterest { Value = interestDecimal });
            }

            var depositInterest = new DepositInterest
            {
                ControlBalances = depositControlBalances,
                Interests = depositControlInterests,
            };

            var conditions = new BankingConditions
            {
                CreditLimit = creditLimit,
                CreditCommission = creditCommission,
                DepositInterest = depositInterest,
                DebitInterest = debitInterest,
            };

            var bank = new Bank
            {
                Id = Guid.NewGuid(),
                Name = bankName,
                Conditions = conditions,
            };

            context.CentralBank.RegisterBank(bank);
            return context;
        }
    }
}