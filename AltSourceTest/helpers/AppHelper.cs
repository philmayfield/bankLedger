using System;
using AltSourceTest.data;
using AltSourceTest.layout;
using AltSourceTest.nav;

namespace AltSourceTest.helpers
{
    public class AppHelper
    {
        public static void Init()
        {
            DataInterface.LogOutUser();
            ResetView();
        }

        public static void Exit()
        {
            DataInterface.LogOutUser();
            Layout.RenderExit();
        }

        public static int GetNavAccess()
        {
            return DataInterface.GetAccount().Length > 0 ? 1 : 0;
        }

        public static void ResetView()
        {
            var navAccess = GetNavAccess();
            var username = DataInterface.GetUsername();

            Console.Clear();
            Layout.RenderTitle();
            Layout.RenderWelcomeMsg(navAccess, username);
            NavigationMenu.RenderMenu(navAccess);
        }
    }
}