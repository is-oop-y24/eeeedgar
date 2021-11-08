using Spectre.Console;

namespace Banks.UI.EntitiesUI
{
    public class Clarifier
    {
        public static string AskString(string whatString)
        {
            return AnsiConsole.Ask<string>($"Enter {whatString}:");
        }

        public static decimal AskDecimal(string whatNumber)
        {
            return AnsiConsole.Ask<decimal>($"Enter {whatNumber}:");
        }
    }
}