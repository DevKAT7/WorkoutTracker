namespace WorkoutTracker.Core.Domain
{
    public class Workout
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public DayOfWeek? Day { get; private set; }
        public int UserId { get; private set; }
        public User User { get; private set; }
        public List<WorkoutExercise> WorkoutExercises { get; private set; }

        public Workout(string name, DayOfWeek? day, int userId)
        {
            Name = name;
            Day = day;
            UserId = userId;
            WorkoutExercises = new List<WorkoutExercise>();
        }

        public void Update(string name, DayOfWeek? day)
        {
            Name = name;
            Day = day;
        }
    }
}
