using WorkoutTracker.Core.Repositories;
using WorkoutTracker.Infrasctructure.DTO;
using WorkoutTracker.Infrasctructure.Mapping;

namespace WorkoutTracker.Infrasctructure.Services
{
    public class WorkoutService : IWorkoutService
    {
        private readonly IWorkoutRepository _workoutRepository;

        public WorkoutService(IWorkoutRepository workoutRepository)
        {
            _workoutRepository = workoutRepository;
        }
        public IEnumerable<WorkoutReadDto> GetAllWorkouts(int userId)
        {
            var workouts = _workoutRepository.GetAll(userId);

            return workouts.Select(x => x.MapToDto());
        }

        public WorkoutReadDto GetWorkoutById(int id)
        {
            var workout = _workoutRepository.Get(id);

            var workoutDto = workout.MapToDto();

            return workoutDto;
        }

        public WorkoutReadDto AddWorkout(WorkoutCreateDto workoutCreateDto)
        {
            var workout = workoutCreateDto.MapToWorkout();

            _workoutRepository.Add(workout);

            _workoutRepository.SaveChanges();

            return workout.MapToDto();
        }

        public void AddExerciseToWorkout(int workoutId, int exerciseId)
        {
            var workout = _workoutRepository.Get(workoutId);

            _workoutRepository.AddExerciseToWorkout(workoutId, exerciseId);

            _workoutRepository.SaveChanges();
        }

        public void DeleteExerciseFromWorkout(int workoutId, int exerciseId)
        {
            var workout = _workoutRepository.Get(workoutId);

            _workoutRepository.DeleteExerciseFromWorkout(workoutId, exerciseId);

            _workoutRepository.SaveChanges();
        }

        public void UpdateWorkout(int id, string name, DayOfWeek? day)
        {
            var workout = _workoutRepository.Get(id);

            workout.Update(name, day);

            _workoutRepository.SaveChanges();
        }

        public void DeleteWorkout(int id)
        {
            var workout = _workoutRepository.Get(id);

            _workoutRepository.Delete(workout);

            _workoutRepository.SaveChanges();
        }
    }
}
