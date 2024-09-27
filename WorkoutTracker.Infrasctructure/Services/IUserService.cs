using WorkoutTracker.Infrasctructure.DTO;

namespace WorkoutTracker.Infrasctructure.Services
{
    public interface IUserService
    {
        void RegisterUser(UserRegisterDto userRegisterDto);
    }
}
