namespace WorkoutTracker.Infrasctructure.DTO
{
    public class WorkoutCreateDto
    {
        public string Name { get; set; }
        public DayOfWeek? Day { get; set; }
        public int UserId { get; set; }
    }
}
