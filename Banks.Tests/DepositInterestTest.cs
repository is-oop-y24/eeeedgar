using System.Collections.Generic;
using Banks.Accounts;
using Banks.Transactions;
using NUnit.Framework;

namespace Banks.Tests
{
    public class DepositInterestTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CheckCorrectBalanceRecognise()
        {
            var controlBalances = new List<decimal>
            {
                10,
                100,
                1000
            };
            var interests = new List<decimal>
            {
                1,
                2,
                3,
                4
            };
            var depositInterest = new DepositInterest(controlBalances, interests);
            Assert.AreEqual(depositInterest.Interest(0), interests[0]);
            Assert.AreEqual(depositInterest.Interest(11), interests[1]);
            Assert.AreEqual(depositInterest.Interest(101), interests[2]);
            Assert.AreEqual(depositInterest.Interest(1001), interests[3]);
        }
    }
}