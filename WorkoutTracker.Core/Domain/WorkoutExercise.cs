namespace WorkoutTracker.Core.Domain
{
    public class WorkoutExercise
    {
        public int WorkoutId { get; private set; }
        public Workout Workout { get; private set; }

        public int ExerciseId { get; private set; }
        public Exercise Exercise { get; private set; }

        public WorkoutExercise(int workoutId, int exerciseId)
        {
            WorkoutId = workoutId;
            ExerciseId = exerciseId;
        }
    }
}
