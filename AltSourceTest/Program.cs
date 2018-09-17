using System;
using AltSourceTest.data;
using AltSourceTest.helpers;
using AltSourceTest.layout;
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
            } while (input != null && input.ToLower() != "q");

            AppHelper.Exit();
        }
    }
}
