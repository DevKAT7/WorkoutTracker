using WorkoutTracker.Core.Domain;
using WorkoutTracker.Core.Repositories;
using WorkoutTracker.Infrasctructure.Data;

namespace WorkoutTracker.Infrasctructure.Repositories
{
    public class WorkoutRepository : IWorkoutRepository
    {
        private readonly WorkoutTrackerContext _context;

        public WorkoutRepository(WorkoutTrackerContext context)
        {
            _context = context;
        }

        public void Add(Workout workout)
        {
            if (workout == null)
            {
                throw new ArgumentNullException(nameof(workout));
            }

            _context.Workouts.Add(workout);
        }

        public void AddExerciseToWorkout(int workoutId, int exerciseId)
        {
            var workout = Get(workoutId);

            if (workout == null)
            {
                throw new ArgumentNullException(nameof(workout));
            }

            var workoutExercise = new WorkoutExercise(workoutId, exerciseId);

            _context.WorkoutExercises.Add(workoutExercise);
        }

        public void DeleteExerciseFromWorkout(int workoutId, int exerciseId)
        {
            var workout = Get(workoutId);

            if (workout == null)
            {
                throw new ArgumentNullException(nameof(workout));
            }

            var workoutExercise = new WorkoutExercise(workoutId, exerciseId);

            _context.WorkoutExercises.Remove(workoutExercise);
        }

        public void Delete(Workout workout)
        {
            if (workout == null)
            {
                throw new ArgumentNullException(nameof(workout));
            }

            var workoutsToDelete = _context.WorkoutExercises.Where(x => x.WorkoutId == workout.Id).ToList();
            foreach (var item in workoutsToDelete)
            {
                _context.WorkoutExercises.Remove(item);
            }

            _context.Workouts.Remove(workout);
        }

        public Workout Get(int id)
        {
            return _context.Workouts.FirstOrDefault(x => x.Id == id);
        }

        public List<Workout> GetAll(int userId)
        {
            return _context.Workouts.Where(x => x.UserId == userId).ToList();
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
