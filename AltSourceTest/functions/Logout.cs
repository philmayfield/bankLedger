using AltSourceTest.data;
using AltSourceTest.helpers;
using System.Collections.Generic;

namespace AltSourceTest.functions
{
    public class Logout
    {
        public Logout(params object[] args)
        {
            var instructions = new List<Instruction>
            {
                new Instruction("Are you sure you are ready to log out? (Y/N)", ValidateLogout)
            };

            FunctionHelper.Init(args, instructions);
        }

        public static bool ValidateLogout(string choice)
        {
            var cleanChoice = StringHelper.CleanInput(choice).ToLower();

            if (!Validation.IsYesOrNo(cleanChoice))
            {
                return false;
            }

            if (cleanChoice == "y" || cleanChoice == "yes")
            {
                // log out
                DataInterface.LogOutUser();
            }

            return true;
        }
    }
}