using System;

namespace AltSourceTest.helpers
{
    public class Validation
    {
        public static bool IsValidTransactionAmt(string input)
        {
            return IsDecimal(input) && DecIsGtz(ToDecimal(input));
        }

        public static bool HasSufficientFunds(string input, decimal balance)
        {
            var amt = ToDecimal(input);
            bool output = balance >= amt;

            if (!output)
            {
                RenderError($"Sorry, there are not sufficient funds to make that withdrawal.");
            }

            return output;
        }

        public static bool IsInt(string input)
        {
            return int.TryParse(input, out var output);
        }

        public static bool IsDecimal(string input)
        {
            bool output = decimal.TryParse(input, out var number);

            if (!output)
            {
                RenderError($"Sorry, {input} is not a number we recognise.");
            }

            return output;
        }

        public static bool DecIsGtz(decimal number)
        {
            var output = number > 0;

            if (!output)
            {
                RenderError($"Sorry, the number must be greater than zero.");
            }

            return output;
        }

        public static bool MinStringLength(string input, int minLength)
        {
            var output = !string.IsNullOrWhiteSpace(input) && input.Length >= minLength;

            if (!output)
            {
                RenderError($"Sorry, the minimum length is {minLength} characters.");
            }

            return output;
        }

        public static bool IsYesOrNo(string dirty)
        {
            var clean = StringHelper.CleanInput(dirty).ToLower();
            return clean == "y" || clean == "yes" || clean == "n" || clean == "no";
        }

        public static bool IsGoBack(string dirty)
        {
            var clean = StringHelper.CleanInput(dirty).ToLower();
            return string.IsNullOrWhiteSpace(clean) || string.Equals(clean, "b");
        }

        public static void RenderSuccess(string input)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n{input}");
            Console.ResetColor();
        }

        public static void RenderError(string input)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n{input}");
            Console.ResetColor();
        }

        private static decimal ToDecimal(string input)
        {
            decimal.TryParse(input, out var output);
            return output;
        }
    }
}