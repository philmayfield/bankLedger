using System.Collections.Generic;

namespace AltSourceTest.nav
{
    /*
     * Source of truth for navigation items
     */
    public class NavList
    {
        public static List<NavItem> NavItems = new List<NavItem>()
            {
                new NavItem("Login", "Log In", 0),
                new NavItem("Register", "Register", 0),
                new NavItem("Deposit", "Make Deposit", 1),
                new NavItem("Withdrawal", "Make Withdrawal", 1),
                new NavItem("Balance", "View Balance", 1),
                new NavItem("History", "View History", 1),
                new NavItem("Logout", "Log Out", 1)
            };
    }
}