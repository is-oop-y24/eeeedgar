using System.Collections.Generic;
using Banks.Model.Entities;
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
            var depositControlPoints = new List<decimal>();
            var depositInterests = new List<decimal>();
            for (int i = 0; i < depositControlPointsNumber; i++)
            {
                decimal depositControlSum = Clarifier.AskDecimal("deposit control sum");
                depositControlPoints.Add(depositControlSum);
                decimal depositInterestUnderControlSum = Clarifier.AskDecimal("deposit interest under control sum");
                depositInterests.Add(depositInterestUnderControlSum);
            }

            {
                decimal depositInterestUnlimited = Clarifier.AskDecimal("deposit interest for unlimited sum");
                depositInterests.Add(depositInterestUnlimited);
            }

            var depositInterest = new DepositInterest(depositControlPoints, depositInterests);

            var conditions = new BankingConditions()
            {
                CreditLimit = creditLimit,
                CreditCommission = creditCommission,
                DepositInterest = depositInterest,
                DebitInterest = debitInterest,
            };

            var bank = new Bank()
            {
                Name = bankName,
                Conditions = conditions,
            };

            context.CentralBank.RegisterBank(bank);
            return context;
        }
    }
}