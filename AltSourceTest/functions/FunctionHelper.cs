using AltSourceTest.helpers;
using AltSourceTest.layout;
using AltSourceTest.nav;
using System;
using System.Collections.Generic;
using System.Text;

namespace AltSourceTest.functions
{
    public class FunctionHelper
    {
        private static NavItem NavItem { get; set; }

        public static void Init(object[] args, List<Instruction> instructions = null, string content = null)
        {
            var navItem = (NavItem)args[0];

            NavItem = navItem;

            Console.Clear();

            /*
             * Render the title area for the function
             */
            Layout.RenderContainedArea(NavItem.Name, '=', true);

            /*
             * Render the content area if applicable
             */
            if (!string.IsNullOrWhiteSpace(content))
            {
                Console.WriteLine(content + "\n");
            }

            /*
             * Render out instructions if applicable
             */
            if (instructions != null && instructions.Count > 0)
            {
                BuildInstructions(instructions);
            }
            else
            {
                string input;
                do
                {
                    Layout.RenderContainedArea(" Enter B) to go Back", '-');
                    input = ConsoleHelper.GetString();
                } while (!string.IsNullOrWhiteSpace(input) && !string.Equals(input, "b"));
            }
        }

        private static void BuildInstructions(List<Instruction> instructions)
        {
            var sb = new StringBuilder();
            var exitingFn = false;

            foreach (var instruction in instructions)
            {
                while (true)
                {
                    sb.Clear()
                        .Append(" ")
                        .Append(instruction.Text);

                    if (!instruction.IsSensitive)
                    {
                        sb.Append(" or B) to go Back");
                    }

                    Layout.RenderContainedArea(sb.ToString(), '-');

                    // Wait for user input
                    var instructionInput = instruction.IsSensitive
                        ? ConsoleHelper.GetPassword()
                        : ConsoleHelper.GetString();

                    if (Validation.IsGoBack(instructionInput))
                    {
                        exitingFn = true;
                        break;
                    }

                    // Send input to callback function
                    bool result = instruction.Action(instructionInput);

                    // result should be truthy to break loop or falsy to stay in loop
                    if (result)
                    {
                        // If the result of the action returns a truthy val,
                        // action was successful and we can break the loop
                        break;
                    }
                }

                if (exitingFn)
                {
                    break;
                }
            }
        }
    }
}