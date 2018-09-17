namespace AltSourceTest.nav
{
    public class NavItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Access { get; set; }

        public NavItem(string id, string name, int access)
        {
            Id = id;
            Name = name;
            Access = access;
        }
    }
}