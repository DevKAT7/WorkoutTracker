namespace WorkoutTracker.Infrasctructure.DTO
{
    public class WorkoutReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Day { get; set; }
        public int UserId { get; set; }
    }
}
