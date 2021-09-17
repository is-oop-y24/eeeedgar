using Shops.Entities;
using Shops.Services;
using Spectre.Console;

namespace Shops.UI
{
    public class ShopManagerUI
    {
        private ShopManager _shopManager;

        private ShopManagerUI(ShopManager shopManager)
        {
            _shopManager = shopManager;
        }

        public static ShopManagerUI CreateInstance(ShopManager shopManager)
        {
            return new ShopManagerUI(shopManager);
        }

        public void DisplayProducts()
        {
            var table = new Table();

            table.Title = new TableTitle("Registered Products");

            table.AddColumns("id", "Product Name");

            foreach (Product product in _shopManager.Products)
            {
                table.AddRow(product.Id.ToString(), product.Name);
            }

            AnsiConsole.Render(table);
        }

        public void DisplayShops()
        {
            var table = new Table();

            table.Title = new TableTitle("Registered Shops");

            table.AddColumns("id", "Shop Name", "Address");

            foreach (Shop shop in _shopManager.Shops)
            {
                table.AddRow(shop.Id.ToString(), shop.Name, shop.Address);
            }

            AnsiConsole.Render(table);
        }

        public void DisplayPersons()
        {
            var table = new Table();

            table.Title = new TableTitle("Registered Persons");

            table.AddColumns("id", "Person Name");

            foreach (Person person in _shopManager.Persons)
            {
                table.AddRow(person.Id.ToString(), person.Name);
            }

            AnsiConsole.Render(table);
        }
    }
}