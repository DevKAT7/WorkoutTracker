using WorkoutTracker.Core.Domain;

namespace WorkoutTracker.Infrasctructure.DTO
{
    public class ExerciseReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Sets { get; set; }
    }
}
