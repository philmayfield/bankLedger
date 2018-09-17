using AltSourceTest.helpers;
using System;
using System.Collections.Generic;
using AltSourceTest.data;

namespace AltSourceTest.functions
{
    public class Register
    {
        private static string Username { get; set; }
        private static string Password { get; set; }

        public Register(params object[] args)
        {
            var instructions = new List<Instruction>()
            {
                new Instruction("Type a username (min 5 characters)", ValidateUsername),
                new Instruction("Type a password (min 5 characters)", ValidateRegistration, true)
            };

            FunctionHelper.Init(args, instructions);
        }

        private static bool ValidateUsername(string username)
        {
            var cleanUsername = StringHelper.CleanInput(username);

            if (!Validation.MinStringLength(cleanUsername, 5)) return false;

            var dataInterface = new DataInterface();
            var usernameExists = dataInterface.UsernameExists(cleanUsername);

            if (usernameExists)
            {
                return false;
            }

            Username = cleanUsername;
            return true;
        }

        private static bool ValidateRegistration(string password)
        {
            var cleanPassword = StringHelper.CleanInput(password);

            if (!Validation.MinStringLength(cleanPassword, 5)) return false;

            Password = cleanPassword;

            var dataInterface = new DataInterface();
            var output = dataInterface.AddUser(Username, Password);

            if (output)
            {
                var user = dataInterface.GetUser(Username, Password);
                Validation.RenderSuccess($"You have registered the user '{user.Username}'!  Press any key to continue.");
                Console.ReadLine();
                return true;
            }

            Validation.RenderError("We ran into an error trying to create your user.");
            return false;
        }
    }
}