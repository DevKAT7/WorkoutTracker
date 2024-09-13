using WorkoutTracker.Core.Domain;
using WorkoutTracker.Infrasctructure.DTO;

namespace WorkoutTracker.Infrasctructure.Mapping
{
    public static class WorkoutExtensions
    {
        public static WorkoutReadDto MapToDto(this Workout workout)
        {
            return new WorkoutReadDto
            {
                Id = workout.Id,
                Name = workout.Name,
                Day = workout.Day.HasValue ? workout.Day.ToString() : null,
                UserId = workout.UserId,
            };
        }

        public static Workout MapToWorkout(this WorkoutCreateDto workoutCreateDto)
        {
            return new Workout(workoutCreateDto.Name, workoutCreateDto.Day, workoutCreateDto.UserId);
        }
    }
}
