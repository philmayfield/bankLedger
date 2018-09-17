using System;
using System.Text;

namespace AltSourceTest.helpers
{
    public class ConsoleHelper
    {
        public static string GetString()
        {
            RenderPrompt();
            var str = Console.ReadLine();
            var clean = StringHelper.CleanInput(str);

            return (string.IsNullOrWhiteSpace(clean)) ? "" : clean;
        }

        public static string GetPassword()
        {
            RenderPrompt();
            var password = new StringBuilder();

            while (true)
            {
                var keyInfo = Console.ReadKey(true);

                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    Console.Write("\n");
                    break;
                }

                if (keyInfo.Key == ConsoleKey.Backspace)
                {
                    if (password.Length > 0)
                    {
                        password.Remove(password.Length - 1, 1);
                        Console.Write("\b \b");
                    }
                }
                else if (keyInfo.KeyChar != '\u0000')
                {
                    password.Append(keyInfo.KeyChar);
                    Console.Write("*");
                }
            }

            return StringHelper.CleanInput(password.ToString());
        }

        private static void RenderPrompt()
        {
//            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("> ");
            Console.ResetColor();
        }
    }
}