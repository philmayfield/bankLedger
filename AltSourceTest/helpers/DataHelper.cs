using System;
using System.Collections.Generic;
using AltSourceTest.data;

namespace AltSourceTest.helpers
{
    public class DataHelper
    {
        private static readonly Random Rando = new Random();

        public static decimal CalculateBalance(List<Transaction> transactions)
        {
            decimal output = 0;

            foreach (var transaction in transactions)
            {
                switch (transaction.Type)
                {
                    case 'D':
                        output += transaction.Amount;
                        break;
                    case 'W':
                        output -= transaction.Amount;
                        break;
                }
            }

            return output;
        }

        public static string GenerateRandomString(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_";
            var text = new char[length];

            for (var i = 0; i < length; i++)
            {
                text[i] = chars[Rando.Next(chars.Length)];
            }

            return new string(text);
        }
    }
}