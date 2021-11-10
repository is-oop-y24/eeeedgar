using System;
using Banks.Model.Entities.DepositStuff;

namespace Banks.Model.Entities
{
    public class BankingConditions
    {
        public Guid Id { get; set; }
        public DepositInterest DepositInterest { get; set; }
        public decimal DebitInterest { get; set; }
        public decimal CreditLimit { get; set; }
        public decimal CreditCommission { get; set; }
        public decimal DoubtfulAccountLimit { get; set; }
    }
}