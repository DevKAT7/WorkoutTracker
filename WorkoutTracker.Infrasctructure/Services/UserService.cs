using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WorkoutTracker.Core.Domain;
using WorkoutTracker.Core.Repositories;
using WorkoutTracker.Infrasctructure.Configuration;
using WorkoutTracker.Infrasctructure.DTO;
using WorkoutTracker.Infrasctructure.Exceptions;
using WorkoutTracker.Infrasctructure.Mapping;

namespace WorkoutTracker.Infrasctructure.Services
{
    public class UserService : IUserService
    {
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IUserRepository _userRepository;
        private readonly AuthenticationSettings _authenticationSettings;

        public UserService(IUserRepository userRepository, IPasswordHasher<User> passwordHasher,
            AuthenticationSettings authenticationSettings)
        {
            _passwordHasher = passwordHasher;
            _userRepository = userRepository;
            _authenticationSettings = authenticationSettings;
        }

        public IEnumerable<UserReadDto> GetAllUsers()
        {
            var users = _userRepository.GetAll();

            return users.Select(x => x.MapToDto());
        }

        public UserReadDto GetUserById(int id)
        {
            var user = _userRepository.Get(id);

            var userDto = user.MapToDto();

            return userDto;
        }

        public void UpdateUser(int id, string userName, string email)
        {
            var user = _userRepository.Get(id);

            user.UpdateUser(userName, email);

            _userRepository.SaveChanges();
        }

        public void DeleteUser(int id)
        {
            var user = _userRepository.Get(id);

            _userRepository.Delete(user);

            _userRepository.SaveChanges();
        }

        public void RegisterUser(UserRegisterDto userRegisterDto)
        {
            var user = userRegisterDto.MapToUser();

            var hashedPassword = _passwordHasher.HashPassword(user, userRegisterDto.Password);

            user.PasswordHash = hashedPassword;

            _userRepository.Add(user);

            _userRepository.SaveChanges();
        }

        public string GenerateJwt(UserLoginDto userLoginDto)
        {
            var user = _userRepository.Get(userLoginDto.Email);

            if (user is null)
            {
                throw new BadRequestException("Invalid username or password.");
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, userLoginDto.Password);

            if (result == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Invalid username or password.");
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.Role.Name),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer,
                _authenticationSettings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: cred);

            var tokenHandler = new JwtSecurityTokenHandler();

            return tokenHandler.WriteToken(token);
        }
    }
}
