using WorkoutTracker.Core.Domain;
using WorkoutTracker.Core.Repositories;
using WorkoutTracker.Infrasctructure.Data;

namespace WorkoutTracker.Infrasctructure.Repositories
{
    public class ExerciseRepository : IExerciseRepository
    {
        private readonly WorkoutTrackerContext _context;

        public ExerciseRepository(WorkoutTrackerContext context)
        {
            _context = context;
        }

        //Adding exercise and adding it to workout
        //public void Add(Exercise exercise, int workoutId)
        //{
        //    if (exercise == null)
        //    {
        //        throw new ArgumentNullException(nameof(exercise));
        //    }

        //    _context.Exercises.Add(exercise);

        //    SaveChanges();

        //    var workoutExercise = new WorkoutExercise(workoutId, exercise.Id);

        //    _context.WorkoutExercises.Add(workoutExercise);

        //    SaveChanges();
        //}

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
            return _context.Exercises.FirstOrDefault(x => x.Id == id);
        }

        public List<Exercise> GetAll(int workoutId)
        {
            return _context.WorkoutExercises.Where(x => x.WorkoutId == workoutId).Select(x => x.Exercise).ToList();
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
