using AltSourceTest.data;
using AltSourceTest.helpers;
using AltSourceTest.layout;

namespace AltSourceTest.functions
{
    public class Balance
    {
        public Balance(params object[] args)
        {
            var dataInterface = new DataInterface();

            // get all transactions for the user
            var transactions = dataInterface.GetTransactions();

            var balance = DataHelper.CalculateBalance(transactions).ToString("C");

            var content = Layout.BuildCenteredText($"The current balance for your account is: {balance}");

            // No instructions needed for blance inquiry
            FunctionHelper.Init(args, null, content);
        }
    }
}