using System.Security.Principal;

namespace WorkoutTracker.Core.Domain
{
    public class User
    {
        public int Id { get; private set; }
        public string UserName { get; private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
        public IEnumerable<Workout> Workouts { get; private set; }
        public int RoleId { get; private set; }
        public Role Role { get; private set; }

        public User(string userName)
        {
            UserName = userName;
            Workouts = new List<Workout>();
        }

        public void UpdateUser(string userName)
        {
            UserName = userName;
        }  
    }
}
