using AltSourceTest.helpers;
using AltSourceTest.nav;
using System;

namespace AltSourceTest.functions
{
    public class FunctionLoader
    {
        public void Load(NavItem navItem)
        {
            /*
             * Method to load up a function based on a selected navItem
             */
            const string nameSpace = "AltSourceTest.functions";
            var theFunc = $"{nameSpace}.{navItem.Id}";
            var catType = Type.GetType(theFunc);
            var args = new object[] { navItem };

            try
            {
                Activator.CreateInstance(catType, args);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new InvalidOperationException();
            }

            /*
             * Once function is done, return to default menu
             */
            AppHelper.ResetView();
        }
    }
}