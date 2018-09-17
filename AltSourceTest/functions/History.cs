using AltSourceTest.helpers;
using System.Collections.Generic;
using System.Text;
using AltSourceTest.data;
using AltSourceTest.layout;

namespace AltSourceTest.functions
{
    public class History
    {
        public History(params object[] args)
        {
            var dataInterface = new DataInterface();

            var history = BuildContent(dataInterface.GetTransactions());

            // No instructions needed for history
            FunctionHelper.Init(args, null, history);
        }

        private static string BuildContent(List<Transaction> transactions)
        {
            if (transactions.Count == 0)
            {
                return Layout.BuildCenteredText("There are no transactions yet for this account.");
            }

            var sb = new StringBuilder();
            sb.AppendLine(MakeTableRow("Date", "Type", "Amount"));
            sb.AppendLine(Layout.BuildFullLine('-'));
            

            foreach (var transaction in transactions)
            {
                var date = $"{transaction.DateAndTime.ToShortDateString()} {transaction.DateAndTime.ToShortTimeString()}";
                var type = transaction.Type == 'D' ? "Deposit" : "Withdrawal";
                var amt = $"{(transaction.Type == 'D' ? "+" : "-")} {transaction.Amount.ToString("C")}";

                sb.AppendLine(MakeTableRow(date, type, amt));
            }

            sb.AppendLine(Layout.BuildFullLine('-'));

            sb.AppendLine(MakeTableRow("Current Balance", null, $"{DataHelper.CalculateBalance(transactions).ToString("C")}"));

            return sb.ToString();
        }

        private static string MakeTableRow(string date, string type, string amt)
        {
            return Layout.BuildCenteredText($"{date, -25}{type, -10}{amt, 20}");
        }
    }
}