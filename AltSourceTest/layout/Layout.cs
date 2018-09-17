using System;
using System.Text;

namespace AltSourceTest.layout
{
    internal class Layout
    {
        private const int AppWidth = 100;

        public static void RenderTitle()
        {
            RenderContainedArea("Welcome to Phils Fargo", '=', true);
        }

        public static void RenderWelcomeMsg(int navAccess, string username)
        {
            var sb = new StringBuilder();

            if (navAccess == 0)
            {
                sb.AppendLine(" Hello and welcome to Phils Fargo, " +
                              "the best banking experience in the land!\n\n" +
                              " Get started today by logging in to your account.\n" +
                              " Dont have an account?  Go ahead and register " +
                              "for a new account now, its FREE!");
            }
            else
            {
                sb.AppendLine($" Welcome back to Phils Fargo {username}!\n");
                sb.AppendLine(" Go ahead and start your banking session by selection an option below");
            }

            Console.WriteLine(sb.ToString());
        }

        public static void RenderExit()
        {
            Console.Clear();
            var sb = new StringBuilder();

            sb.AppendLine()
                .AppendLine(BuildCenteredText("Exiting the App!"))
                .AppendLine(BuildCenteredText("----------------"))
                .AppendLine(BuildCenteredText("Keep in mind that I built this with about a week and a halfs"))
                .AppendLine(BuildCenteredText("worth of experience with C#.NET, so please be gentile."))
                .AppendLine()
                .AppendLine(BuildCenteredText("Thanks for the consideration! :) -Phil"));

            RenderContainedArea(sb.ToString(), '+');
        }

        public static string BuildFullLine(char charRepeat)
        {
            var sb = new StringBuilder();
            return sb.Append(charRepeat, AppWidth).ToString();
        }

        public static void RenderFullLine(char charRepeat)
        {
            Console.WriteLine(BuildFullLine(charRepeat));
        }

        public static void RenderContainedArea(string text, char charRepeat, bool centered = false)
        {
            var sb = new StringBuilder();
            var renderedText = centered ? BuildCenteredText(text) : text;

            sb.Append(charRepeat, AppWidth)
                .Append('\n')
                .AppendLine(renderedText)
                .Append(charRepeat, AppWidth)
                .Append('\n');

            Console.WriteLine(sb.ToString());
        }

        public static string BuildCenteredText(string text, int width = AppWidth)
        {
            var sb = new StringBuilder();
            int space = (AppWidth - text.Length) / 2;

            sb.Append(' ', space)
                .Append(text)
                .Append(' ', space);

            return sb.ToString();
        }
    }
}