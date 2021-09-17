using Shops.Entities;
using Shops.Services;
using Shops.UI;

namespace Shops
{
    internal class Program
    {
        private static void Main()
        {
            var market = ShopManager.CreateInstance();
            Product corn = market.RegisterProduct("corn");
            Product pop = market.RegisterProduct("pop");
            Product carrot = market.RegisterProduct("carrot");

            Shop spar = market.RegisterShop("Spar", "Street");

            spar.AddPosition(corn);

            Person edgar = market.RegisterPerson("Edgar");
            edgar.AddItemToWishList(corn, 5);
            market.Bank.GiveMoney(2, 100);

            var bankUi = BankUI.CreateInstance(market.Bank);
            var shopManagerUi = ShopManagerUI.CreateInstance(market);
            var shopUi = ShopUI.CreateInstance(spar);
            var personUi = PersonUI.CreateInstance(edgar);

            bankUi.DisplayProfiles();

            shopManagerUi.DisplayProducts();
            shopManagerUi.DisplayPersons();
            shopManagerUi.DisplayShops();

            shopUi.DisplayStock();

            personUi.DisplayWishList();

            market.MakeDeal(edgar, spar);
        }
    }
}