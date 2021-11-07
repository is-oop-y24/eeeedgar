using Spectre.Console;

namespace Banks.UI.EntitiesUI
{
    public class Clarifier
    {
        public static string AskString(string whatString)
        {
            return AnsiConsole.Ask<string>($"Enter {whatString}:");
        }

        public static int AskNumber(string whatNumber)
        {
            return AnsiConsole.Ask<int>($"Enter {whatNumber}:");
        }
    }
}