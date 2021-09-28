using System.Collections.Generic;
using Spectre.Console;

namespace Shops.UI
{
    public class PersonUi
    {
        public static string Menu(string name)
        {
            var commands = new List<string>
            {
                "Show Shop Stock",
                "Buy",
                "Back to Shop Manager",
            };

            string choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title($"Person {name}")
                    .PageSize(10)
                    .AddChoices(commands));
            AnsiConsole.Clear();

            return choice;
        }
    }
}