using System;
using System.Collections.Generic;
using Shops.Entities;
using Shops.Services;

namespace Shops
{
    internal class Program
    {
        private static void Main()
        {
            var market = MarketDatabase.CreateInstance();
            Product corn = market.RegisterProduct("corn");
            Shop spar = market.RegisterShop("spar", "furshtatskaya");
        }
    }
}