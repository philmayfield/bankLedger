using AltSourceTest.helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using AltSourceTest.data;

namespace AltSourceTest.functions
{
    public class Deposit
    {
        private static string Amount { get; set; }

        public Deposit(params object[] args)
        {
            var content = " Make a deposit to your account, we use magic money so it disappears from your wallet right away!";

            var instructions = new List<Instruction>()
            {
                new Instruction("How much are you depositing?", DepositCash),
            };

            FunctionHelper.Init(args, instructions, content);
        }

        public static bool DepositCash(string amount)
        {
            var cleanAmount = StringHelper.CleanInput(amount);

            if (!Validation.IsValidTransactionAmt(cleanAmount))
            {
                return false;
            }

            Amount = cleanAmount;

            var amt = decimal.Parse(Amount, NumberStyles.Currency);
            var dataInterface = new DataInterface();

            // AddTransaction will return bool based on insert success / fail
            var output = dataInterface.AddTransaction('D', amt);

            if (output)
            {
                Validation.RenderSuccess($"Deposit of {amt.ToString("C")} was successful!  Press any key to continue.");
                Console.ReadLine();
                return true;
            }

            Validation.RenderError("We ran into an error trying to make the deposit.");
            return false;
        }
    }
}