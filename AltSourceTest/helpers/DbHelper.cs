using System.Configuration;

namespace AltSourceTest.helpers
{
    public class DbHelper
    {
        public static string ConnectionValue(string text)
        {
            return ConfigurationManager.ConnectionStrings[text].ConnectionString;
        }
    }
}