using WorkoutTracker.Core.Domain;

namespace WorkoutTracker.Core.Repositories
{
    public interface IWorkoutRepository
    {
        Workout Get(int id);
        List<Workout> GetAll(int userId);
        void Add(Workout workout);
        void AddExerciseToWorkout(int workoutId, int exerciseId);
        void DeleteExerciseFromWorkout(int workoutId, int exerciseId);
        void Delete(Workout workout);
        bool SaveChanges();
    }
}
