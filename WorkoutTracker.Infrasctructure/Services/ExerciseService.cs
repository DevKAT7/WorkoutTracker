using WorkoutTracker.Core.Repositories;
using WorkoutTracker.Infrasctructure.DTO;
using WorkoutTracker.Infrasctructure.Mapping;

namespace WorkoutTracker.Infrasctructure.Services
{
    public class ExerciseService : IExerciseService
    {
        private readonly IExerciseRepository _exerciseRepository;

        public ExerciseService(IExerciseRepository exerciseRepository)
        {
            _exerciseRepository = exerciseRepository;
        }

        public ExerciseReadDto GetExerciseById(int id)
        {
            var exercise = _exerciseRepository.Get(id);

            var exerciseReadDto = exercise.MapToDto();

            return exerciseReadDto;
        }

        public IEnumerable<ExerciseReadDto> GetAllExercises(int workoutId)
        {
            var exercises = _exerciseRepository.GetAll(workoutId);

            return exercises.Select(x => x.MapToDto());
        }

        public ExerciseReadDto AddExercise(ExerciseCreateDto exerciseCreateDto)
        {
            var exercise = exerciseCreateDto.MapToExercise();

            _exerciseRepository.Add(exercise);

            _exerciseRepository.SaveChanges();

            return exercise.MapToDto();
        }

        public void UpdateExercise(int id, string name, int? sets)
        {
            var exercise = _exerciseRepository.Get(id);

            exercise.Update(name, sets);

            _exerciseRepository.SaveChanges();
        }

        public void DeleteExercise(int id)
        {
            var exercise = _exerciseRepository.Get(id);

            _exerciseRepository.Delete(exercise);

            _exerciseRepository.SaveChanges();
        }
    }
}
