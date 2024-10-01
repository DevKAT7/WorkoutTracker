namespace WorkoutTracker.Infrasctructure.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base(message)
        { 
        }
    }
}
