using Shops.Entities;
using Spectre.Console;

namespace Shops.UI
{
    public class ShopUI
    {
        private Shop _shop;

        private ShopUI(Shop shop)
        {
            _shop = shop;
        }

        public static ShopUI CreateInstance(Shop shop)
        {
            return new ShopUI(shop);
        }

        public void DisplayStock()
        {
            var table = new Table();

            table.Title = new TableTitle($"Shop {_shop.Id} {_shop.Name} Stock");

            table.AddColumns("id", "Product Name", "Amount", "Cost");

            foreach (Position position in _shop.Stock)
            {
                table.AddRow(position.Product.Id.ToString(), position.Product.Name, position.Amount.ToString(), position.Cost.ToString());
            }

            AnsiConsole.Render(table);
        }
    }
}