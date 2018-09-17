using AltSourceTest.helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace AltSourceTest.data
{
    public class DataInterface
    {
        private static readonly string UserPath = $"{Path.GetTempPath()}altsourcetest_user.bin";
        private static readonly string UsersPath = $"{Path.GetTempPath()}altsourcetest_users.bin";
        private static readonly string TransactionsPath = $"{Path.GetTempPath()}altsourcetest_transactions.bin";

        private static string Account { get; set; }
        private static List<Transaction> Transactions { get; set; }
        private static List<User> Users { get; set; }
        private static User User { get; set; }

        public DataInterface()
        {
            Account = GetAccount();
            Transactions = DeserializeTransactions();
            Users = DeserializeUsers();
            User = DeserializeUser();
        }

        /*
         * READ a username and return a bool of whether they exist in the DB
         */
        public bool UsernameExists(string username)
        {
            var cleanUsername = StringHelper.CleanInput(username);

            if (string.IsNullOrWhiteSpace(cleanUsername))
            {
                throw new ArgumentException("The username field cannot be empty.");
            }

            var userExists = Users.Exists(user => user.Username == cleanUsername);

            if (userExists)
            {
                Validation.RenderError($"Sorry the username {username} is already taken. Try a different username please.");

            }

            return userExists;

            /*
             * Database Solution
             *
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DbHelper.ConnectionValue("AltSourceTestDB")))
            {
                var foundUser = connection.Query<int>(
                    "dbo.SP_USERNAME_EXISTS @Username",
                    new { Username = username }
                ).ToList()[0];

                // username not found
                if (foundUser != 1) return false;

                // username found
                Validation.RenderError($"Sorry the username {username} is already taken. Try a different username please.");
                return true;
            }
             */
        }

        /*
         * READ / login a user and save a file with some deets
         */
        public User GetUser(string username, string password)
        {
            var cleanUsername = StringHelper.CleanInput(username);
            var cleanPassword = StringHelper.CleanInput(password);

            if (string.IsNullOrWhiteSpace(cleanUsername) || string.IsNullOrWhiteSpace(cleanPassword))
            {
                throw new ArgumentException("The username and password fields cannot be empty.");
            }

            bool Predicate(User user) => user.Username == cleanUsername && user.Password == cleanPassword;

            var userExists = Users.Exists(Predicate);

            if (!userExists)
            {
                return new User();
            }

            var foundUser = Users.Find(Predicate);

            // set user prop
            User = foundUser;
            // serialize to bin file
            SerializeUser();

            return foundUser;

            /*
             * Database Solution
             *
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DbHelper.ConnectionValue("AltSourceTestDB")))
            {
                var output = connection.Query<User>(
                    "dbo.SP_GET_USER @Username, @Password",
                    new { Username = username, Password = password }
                ).ToList();

                // no user found, return blank User
                if (output.Count != 1)
                {
                    return new User();
                }

                // found the user, return it
                var user = output[0];
                CreateLoginFile(user);
                return user;
            }
             */
        }

        /*
         * Log out user
         */
        public static void LogOutUser()
        {
            User = new User();
            DeleteLoginFile();
        }


        /*
         * CREATE a transaction
         */
        public bool AddTransaction(char type, decimal amount = 0)
        {
            if (type != 'D' && type != 'W')
            {
                throw new ArgumentException("The transaction type must be 'D' for deposit or 'W' for withdrawal.");
            }

            if (amount <= 0)
            {
                throw new ArgumentException("The transaction amount must be greater than zero.");
            }

            var transaction = new Transaction
            {
                Account = Account,
                Type = type,
                Amount = amount,
                DateAndTime = DateTime.Now
            };

            Transactions.Add(transaction);
            SerializeTransactions();

            return true;

            /*
             * Database Solution
             *
            int output;
            var transactions = new List<Transaction>();
            transactions.Add(transaction);

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DbHelper.ConnectionValue("AltSourceTestDB")))
            {
                output = connection.Execute(
                    "dbo.SP_ADD_TRANSACTION @Account, @Type, @Amount, @DateAndTime",
                    transactions);
            }

            // should return rows affected
            return output == 1;
            */
        }

        /*
         * READ a list of transactions
         */
        public List<Transaction> GetTransactions()
        {
            return Transactions.FindAll(transaction => transaction.Account == Account);

            /*
             * Database Solution
             *
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DbHelper.ConnectionValue("AltSourceTestDB")))
            {
                var output = connection.Query<Transaction>(
                    "dbo.SP_GET_TRANSACTIONS @Account",
                    new { Account = Account }
                    ).ToList();
                return output;
            }
            */
        }

        /*
         * CREATE a user on the DB
         */
        public bool AddUser(string username, string password)
        {

            /*
             *  I know we should be salting and hashing the password when saving, but for the simplicity of the exercise we'll skip that for now
             */

            var cleanUsername = StringHelper.CleanInput(username);
            var cleanPassword = StringHelper.CleanInput(password);

            if (string.IsNullOrWhiteSpace(cleanUsername) || string.IsNullOrWhiteSpace(cleanPassword))
            {
                throw new ArgumentException("The username and password fields cannot be empty.");
            }

            // check the list to see if the username already exists

            var usernameExists = Users.Exists(user => user.Username == cleanUsername);

            if (usernameExists)
            {
                Validation.RenderError($"Sorry the username {username} is already taken. Try a different username please.");
                return false;
            }

            string account;
            bool acctExists;

            do
            {
                account = DataHelper.GenerateRandomString(10);

                // check in list to see if the generated account number already exists
                acctExists = Users.Exists(user => user.Account == account);
            } while (acctExists);

            var newUser = new User()
            {
                Account = account,
                Username = cleanUsername,
                Password = cleanPassword
            };

            Users.Add(newUser);
            SerializeUsers();

            return true;

            /*
             * Database Solution
             *
            int output;
            var users = new List<User>();

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DbHelper.ConnectionValue("AltSourceTestDB")))
            {
                // check in DB to see if the input username already exists
                var foundUser = connection.Query<int>(
                    "dbo.SP_USERNAME_EXISTS @Username",
                    new { Username = username }
                    ).ToList()[0];

                if (foundUser == 1)
                {
                    Validation.RenderError($"Sorry the username {username} is already taken. Try a different username please.");
                    return false;
                }

                int foundAcct;
                string account;

                do
                {
                    account = DataHelper.GenerateRandomString(10);

                    // check in DB to see if the generated account number already exists
                    foundAcct = connection.Query<int>(
                        "dbo.SP_ACCOUNT_EXISTS @Account",
                        new { Account = account }
                        ).ToList()[0];
                } while (foundAcct == 1);

                users.Add(new User { Account = account, Username = username, Password = password });

                output = connection.Execute(
                    "dbo.SP_ADD_USER @Account, @Username, @Password",
                    users);
            }

            // should return rows affected
            return output == 1;
             */
        }

        /*
         * READ temp file to get user acct
         */
        public static string GetAccount()
        {
            User = DeserializeUser();
            return User.Account.Length > 0 ? User.Account : "";
        }

        /*
         * READ temp file to get user acct
         */
        public static string GetUsername()
        {
            User = DeserializeUser();
            return User.Username.Length > 0 ? User.Username : "";
        }

        /*
         * Serialize bin file of all transactions.
         */
        private static void SerializeTransactions()
        {
            using (Stream stream = File.Open(TransactionsPath, FileMode.OpenOrCreate))
            {
                var serializer = new BinaryFormatter();
                serializer.Serialize(stream, Transactions);
            }
        }

        /*
         * Deserialize bin file of all transactions.
         */
        private static List<Transaction> DeserializeTransactions()
        {
            // no file to deserialize, make a new list
            if (!File.Exists(TransactionsPath)) return new List<Transaction>();

            // file exists, lets deserialize it!
            using (Stream stream = File.Open(TransactionsPath, FileMode.Open))
            {
                var serializer = new BinaryFormatter();
                return (List<Transaction>)serializer.Deserialize(stream);
            }
        }

        /*
         * Serialize bin file of all users.
         */
        private static void SerializeUsers()
        {
            using (Stream stream = File.Open(UsersPath, FileMode.OpenOrCreate))
            {
                var serializer = new BinaryFormatter();
                serializer.Serialize(stream, Users);
            }
        }

        /*
         * Deserialize bin file of all users.
         */
        private static List<User> DeserializeUsers()
        {
            // no file to deserialize, make a new list
            if (!File.Exists(UsersPath)) return new List<User>();

            // file exists, lets deserialize it!
            using (Stream stream = File.Open(UsersPath, FileMode.Open))
            {
                var serializer = new BinaryFormatter();
                return (List<User>)serializer.Deserialize(stream);
            }
        }

        /*
         * Serialize bin file for user.
         */
        private static void SerializeUser()
        {
            using (Stream stream = File.Open(UserPath, FileMode.OpenOrCreate))
            {
                var serializer = new BinaryFormatter();
                serializer.Serialize(stream, User);
            }
        }

        /*
         * Deserialize bin file for user.
         */
        private static User DeserializeUser()
        {
            // no file to deserialize, make a new list
            if (!File.Exists(UserPath)) return new User();

            // file exists, lets deserialize it!
            using (Stream stream = File.Open(UserPath, FileMode.Open))
            {
                var serializer = new BinaryFormatter();
                return (User)serializer.Deserialize(stream);
            }
        }

        /*
         * DELETE the temp file which stores user deets
         */
        private static void DeleteLoginFile()
        {
            if (File.Exists(UserPath))
            {
                File.Delete(UserPath);
            }
        }
    }
}