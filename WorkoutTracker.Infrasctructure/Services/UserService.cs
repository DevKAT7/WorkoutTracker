using Microsoft.AspNetCore.Identity;
using WorkoutTracker.Core.Domain;
using WorkoutTracker.Core.Repositories;
using WorkoutTracker.Infrasctructure.DTO;
using WorkoutTracker.Infrasctructure.Mapping;

namespace WorkoutTracker.Infrasctructure.Services
{
    public class UserService : IUserService
    {
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository, IPasswordHasher<User> passwordHasher)
        {
            _passwordHasher = passwordHasher;
            _userRepository = userRepository;
        }

        public void RegisterUser(UserRegisterDto userRegisterDto)
        {
            var user = userRegisterDto.MapToUser();

            var hashedPassword = _passwordHasher.HashPassword(user, userRegisterDto.Password);

            user.PasswordHash = hashedPassword;

            _userRepository.Add(user);

            _userRepository.SaveChanges();
        }
    }
}
