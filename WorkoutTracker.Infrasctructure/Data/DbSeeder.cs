using WorkoutTracker.Core.Domain;

namespace WorkoutTracker.Infrasctructure.Data
{
    public class DbSeeder
    {
        private readonly WorkoutTrackerContext _context;

        public DbSeeder(WorkoutTrackerContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            if (_context.Database.CanConnect())
            {
                if(!_context.Roles.Any())
                {
                    var roles = GetRoles();
                    _context.Roles.AddRange(roles);
                    _context.SaveChanges();
                }
            }
        }

        private IEnumerable<Role> GetRoles()
        {
            var roles = new List<Role>()
            {
                new Role("User"),
                new Role("Manager"),
                new Role("Admin"),
            };

            return roles;      
        }
    }
}
