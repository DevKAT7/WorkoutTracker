namespace WorkoutTracker.Core.Domain
{
    public class Role
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public List<User> Users { get; private set; }

        public Role(string name)
        {
            Name = name;
            Users = new List<User>();
        }
    }
    
}
