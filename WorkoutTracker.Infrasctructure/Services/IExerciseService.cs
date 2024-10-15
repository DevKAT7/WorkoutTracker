using WorkoutTracker.Infrasctructure.DTO;

namespace WorkoutTracker.Infrasctructure.Services
{
    public interface IExerciseService
    {
        public ExerciseReadDto GetExerciseById(int id);
        public IEnumerable<ExerciseReadDto> GetAllExercises(int workoutId);
        public ExerciseReadDto AddExercise(ExerciseCreateDto exerciseCreateDto);
        public void UpdateExercise(int id, string name, int? sets);
        public void DeleteExercise(int id);
    }
}
