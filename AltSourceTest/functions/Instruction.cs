using System;

namespace AltSourceTest.functions
{
    public class Instruction
    {
        public string Text { get; set; }
        public Func<string, bool> Action { get; set; }
        public bool IsSensitive { get; set; }

        public Instruction(string text, Func<string, bool> action, bool isSensitive = false)
        {
            Text = text;
            Action = action;
            IsSensitive = isSensitive;
        }
    }
}