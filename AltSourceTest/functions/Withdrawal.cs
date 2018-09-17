using AltSourceTest.helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using AltSourceTest.data;
using AltSourceTest.layout;

namespace AltSourceTest.functions
{
    public class Withdrawal
    {
        private static string Amount { get; set; }
        private static List<Transaction> Transactions { get; set; }
        private static decimal Balance { get; set; }

        public Withdrawal(params object[] args)
        {
            var dataInterface = new DataInterface();

            Transactions = dataInterface.GetTransactions();
            Balance = DataHelper.CalculateBalance(Transactions);

            var sb = new StringBuilder();
            sb.AppendLine(" Make a withdrawal from your account, " +
                          "we use magic money so it shows up in your wallet right away!\n");

            sb.AppendLine(Layout.BuildCenteredText(
                Balance > 0
                    ? $"The current balance for your account is: {Balance.ToString("C")}"
                    : "There is not enough in your account to make a withdrawal :("));

            List<Instruction> instructions = null;
           
            if (Balance > 0)
            {
                instructions = new List<Instruction>()
                {
                    new Instruction("How much are you withdrawing?", GetCash),
                };
            }

            FunctionHelper.Init(args, instructions, sb.ToString());
        }

        public static bool GetCash(string amount)
        {
            var cleanAmount = StringHelper.CleanInput(amount);

            if (!Validation.IsValidTransactionAmt(cleanAmount) || !Validation.HasSufficientFunds(cleanAmount, Balance))
            {
                return false;
            }

            Amount = cleanAmount;

            var amt = decimal.Parse(Amount, NumberStyles.Currency);
            var dataInterface = new DataInterface();

            // AddTransaction will return bool based on insert success / fail
            var output = dataInterface.AddTransaction('W', amt);

            if (output)
            {
                Validation.RenderSuccess($"Withdrawal of {amt.ToString("C")} was successful!  Press any key to continue.");
                Console.ReadLine();
                return true;
            }

            Validation.RenderError("We ran into an error trying to make the withdrawal.");
            return false;
        }
    }
}