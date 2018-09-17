using System;
using System.Collections.Generic;
using System.Text;
using AltSourceTest.functions;
using AltSourceTest.layout;

namespace AltSourceTest.nav
{
    public class NavigationMenu
    {
        private static readonly List<NavItem> ShortList = new List<NavItem>();

        /*
         * Render the menu items to the console
         */
        public static void RenderMenu(int navAccess = 0)
        {
            BuildMenu(navAccess);

            var sb = new StringBuilder();

            Console.WriteLine("\n\n Main Menu: select an option by " +
                              "typing the corresponding number");

            for (var i = 0; i < ShortList.Count; i++)
            {
                sb.Append($" {i + 1}) {ShortList[i].Name} |");
            }
            sb.Append(" Q) Exit");

            Layout.RenderContainedArea(sb.ToString(), '-');
        }

        /*
         * Populate the short list of nav items from the full list
         * based on the users access.
         */
        private static void BuildMenu(int navAccess)
        {
            ShortList.Clear();

            foreach (var navItem in NavList.NavItems)
            {
                if (navItem.Access == navAccess)
                {
                    ShortList.Add(navItem);
                }
            }
        }

        /*
         * Handle the input from the user on a menu
         */
        public static void HandleMenuSelection(string selChar)
        {
            if (!IsValidMenuSelection(selChar)) return;

            var selection = MenuToNum(selChar);
            var menuItem = ShortList[selection - 1];
            var functionLoader = new FunctionLoader();

            functionLoader.Load(menuItem);
        }

        /*
         * Test to see if menu selection is valid
         */
        public static bool IsValidMenuSelection(string selChar)
        {
            var selection = MenuToNum(selChar);

            return selection > 0 && selection <= ShortList.Count;
        }

        private static int MenuToNum(string input)
        {
            var isNumber = int.TryParse(input, out var output);

            return isNumber ? output : -1;
        }
    }
}