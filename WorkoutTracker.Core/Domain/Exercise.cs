namespace WorkoutTracker.Core.Domain
{
    public class Exercise
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public int? Sets { get; private set; }
        public IEnumerable<WorkoutExercise> WorkoutExercises { get; private set; }

        public Exercise(string name, int? sets)
        {
            Name = name;
            Sets = sets;
        }

        public void Update(string name, int? sets)
        {
            Name = name;
            Sets = sets;
        }
    }
}
