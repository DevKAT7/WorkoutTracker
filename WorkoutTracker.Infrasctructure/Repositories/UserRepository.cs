using WorkoutTracker.Core.Domain;
using WorkoutTracker.Core.Repositories;
using WorkoutTracker.Infrasctructure.Data;

namespace WorkoutTracker.Infrasctructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly WorkoutTrackerContext _context;

        public UserRepository(WorkoutTrackerContext context)
        {
            _context = context;
        }

        public void Add(User user)
        {
            if(user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            _context.Users.Add(user);
        }

        public void Delete(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            _context.Users.Remove(user);
        }

        public User Get(int id)
        {
            return _context.Users.FirstOrDefault(x => x.Id == id);
        }

        public List<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
