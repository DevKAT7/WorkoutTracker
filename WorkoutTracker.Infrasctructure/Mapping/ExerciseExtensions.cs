using WorkoutTracker.Core.Domain;
using WorkoutTracker.Infrasctructure.DTO;

namespace WorkoutTracker.Infrasctructure.Mapping
{
    public static class ExerciseExtensions
    {
        public static ExerciseReadDto MapToDto(this Exercise exercise)
        {
            return new ExerciseReadDto
            {
                Id = exercise.Id,
                Name = exercise.Name,
                Sets = exercise.Sets.HasValue ? exercise.Sets : null,
            };
        }

        public static Exercise MapToExercise(this ExerciseCreateDto exerciseCreateDto)
        {
            return new Exercise(exerciseCreateDto.Name, exerciseCreateDto.Sets);
        }
    }
}
