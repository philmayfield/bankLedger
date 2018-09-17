using System;

namespace AltSourceTest.data
{
    [Serializable]
    public class Transaction
    {
        public string Account { get; set; }
        public char Type { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateAndTime { get; set; }
    }
}