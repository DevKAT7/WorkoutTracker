using WorkoutTracker.Infrasctructure.DTO;

namespace WorkoutTracker.Infrasctructure.Services
{
    public interface IWorkoutService
    {
        IEnumerable<WorkoutReadDto> GetAllWorkouts(int userId);
        WorkoutReadDto GetWorkoutById(int id);
        public WorkoutReadDto AddWorkout(WorkoutCreateDto workoutCreateDto);
        public void AddExerciseToWorkout(int workoutId, int exerciseId);
        public void DeleteExerciseFromWorkout(int workoutId, int exerciseId);
        public void UpdateWorkout(int id, string name, DayOfWeek? day);
        public void DeleteWorkout(int id);
    }
}
