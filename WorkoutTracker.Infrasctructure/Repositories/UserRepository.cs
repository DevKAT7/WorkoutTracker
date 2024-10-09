using Microsoft.EntityFrameworkCore;
using WorkoutTracker.Core.Domain;
using WorkoutTracker.Core.Repositories;
using WorkoutTracker.Infrasctructure.Data;
using WorkoutTracker.Infrasctructure.Exceptions;

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
            var user = _context.Users.FirstOrDefault(x => x.Id == id);

            if (user is null)
            {
                throw new BadRequestException("Invalid Id.");
            }

            return user;
        }

        public User Get(string email)
        {
            return _context.Users
                .Include(u => u.Role)
                .FirstOrDefault(x => x.Email == email);
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
