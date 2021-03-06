using AltSourceTest.helpers;
using AltSourceTest.nav;

namespace AltSourceTest
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            AppHelper.Init();

            string input;

            do
            {
                // wait for user input
                input = ConsoleHelper.GetString();

                if (NavigationMenu.IsValidMenuSelection(input))
                {
                    NavigationMenu.HandleMenuSelection(input);
                }
                else
                {
                    AppHelper.ResetView();
                }
            } while (input != null && input.ToLower() != "quit");

            AppHelper.Exit();
        }
    }
}
