using System.Text;
using System.Text.RegularExpressions;

namespace AltSourceTest.helpers
{
    public class StringHelper
    {
        //by MSDN 
        public static string CleanInput(string strIn)
        {
            // Replace invalid characters with empty strings.
            return Regex.Replace(strIn, @"[^\w\.@-]", "").Trim();
        }
    }
}