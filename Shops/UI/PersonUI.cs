using Shops.Entities;
using Spectre.Console;

namespace Shops.UI
{
    public class PersonUI
    {
        private Person _person;

        private PersonUI(Person person)
        {
            _person = person;
        }

        public static PersonUI CreateInstance(Person person)
        {
            return new PersonUI(person);
        }

        public void DisplayWishList()
        {
            var table = new Table();

            table.Title = new TableTitle($"Person {_person.Id} {_person.Name} WishList");

            table.AddColumns("id", "Product Name", "Amount");

            foreach (Purchase purchase in _person.WishList)
            {
                table.AddRow(purchase.Product.Id.ToString(), purchase.Product.Name, purchase.Amount.ToString());
            }

            AnsiConsole.Render(table);
        }
    }
}