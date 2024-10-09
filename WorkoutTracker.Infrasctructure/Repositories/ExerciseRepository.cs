using WorkoutTracker.Core.Domain;
using WorkoutTracker.Core.Repositories;
using WorkoutTracker.Infrasctructure.Data;
using WorkoutTracker.Infrasctructure.Exceptions;

namespace WorkoutTracker.Infrasctructure.Repositories
{
    public class ExerciseRepository : IExerciseRepository
    {
        private readonly WorkoutTrackerContext _context;

        public ExerciseRepository(WorkoutTrackerContext context)
        {
            _context = context;
        }

        public void Add(Exercise exercise)
        {
            if (exercise == null)
            {
                throw new ArgumentNullException(nameof(exercise));
            }

            _context.Exercises.Add(exercise);
        }

        public void Delete(Exercise exercise)
        {
            if (exercise == null)
            {
                throw new ArgumentNullException(nameof(exercise));
            }

            var exercisesToDelete = _context.WorkoutExercises.Where(x => x.ExerciseId == exercise.Id).ToList();

            foreach (var item in exercisesToDelete)
            {
                _context.WorkoutExercises.Remove(item);
            }

            _context.Exercises.Remove(exercise);
        }

        public Exercise Get(int id)
        {
            var exercise = _context.Exercises.FirstOrDefault(x => x.Id == id);

            if (exercise is null)
            {
                throw new BadRequestException("Invalid Id.");
            }

            return exercise;
        }

        public List<Exercise> GetAll(int workoutId)
        {
            var workout = _context.Workouts.FirstOrDefault(x => x.Id == workoutId);

            if (workout is null)
            {
                throw new BadRequestException("Invalid workout Id.");
            }

            return _context.WorkoutExercises.Where(x => x.WorkoutId == workoutId).Select(x => x.Exercise).ToList();
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
