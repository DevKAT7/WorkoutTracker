using WorkoutTracker.Core.Domain;

namespace WorkoutTracker.Core.Repositories
{
    public interface IUserRepository
    {
        User Get(int id);
        List<User> GetAll();
        void Add(User user);
        void Delete(User user);
        bool SaveChanges();
    }
}
