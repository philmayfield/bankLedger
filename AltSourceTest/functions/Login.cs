using AltSourceTest.helpers;
using System;
using System.Collections.Generic;
using AltSourceTest.data;

namespace AltSourceTest.functions
{
    public class Login
    {
        private static string Username { get; set; }
        private static string Password { get; set; }

        public Login(params object[] args)
        {
            var instructions = new List<Instruction>()
            {
                new Instruction("Type your username", StoreUsername),
                new Instruction("Type your password", ValidateLogin, true)
            };

            FunctionHelper.Init(args, instructions);
        }

        public static bool StoreUsername(string username)
        {
            var cleanUsername = StringHelper.CleanInput(username);

            if (!Validation.MinStringLength(cleanUsername, 5)) return false;

            Username = cleanUsername;
            return true;
        }

        public static bool ValidateLogin(string password)
        {
            var cleanPassword = StringHelper.CleanInput(password);

            if (!Validation.MinStringLength(cleanPassword, 5)) return false;

            Password = cleanPassword;

            var dataInterface = new DataInterface();
            var output = dataInterface.GetUser(Username, Password);

            if (!string.IsNullOrWhiteSpace(output.Account))
            {
                // success
                Validation.RenderSuccess($"Welcome back {Username}, you're logged in! Press any key to continue.");
                Console.ReadLine();
                return true;
            }

            // fail
            Validation.RenderError("Sorry we didnt find an account with that username and password. Press any key to try again.");
            Console.ReadKey(true);
            return true;
        }
    }
}