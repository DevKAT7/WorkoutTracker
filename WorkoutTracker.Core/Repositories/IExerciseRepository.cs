using WorkoutTracker.Core.Domain;

namespace WorkoutTracker.Core.Repositories
{
    public interface IExerciseRepository
    {
        Exercise Get(int id);
        List<Exercise> GetAll(int workoutId);
        void Add(Exercise exercise);
        //void Add(Exercise exercise, int workoutId);
        void Delete(Exercise exercise);
        bool SaveChanges();
    }
}
