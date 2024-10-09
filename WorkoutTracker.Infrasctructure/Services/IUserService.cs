using Microsoft.AspNetCore.Mvc;
using WorkoutTracker.Infrasctructure.DTO;

namespace WorkoutTracker.Infrasctructure.Services
{
    public interface IUserService
    {
        IEnumerable<UserReadDto> GetAllUsers();
        UserReadDto GetUserById(int id);
        void UpdateUser(int id, string userName, string email);
        void RegisterUser(UserRegisterDto userRegisterDto);
        string GenerateJwt(UserLoginDto userLoginDto);
        void DeleteUser(int id);
    }
}
