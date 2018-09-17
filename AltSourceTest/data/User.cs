using System;

namespace AltSourceTest.data
{
    [Serializable]
    public class User
    {
//        public int Id { get; set; }
        public string Account { get; set; } = "";
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
    }
}