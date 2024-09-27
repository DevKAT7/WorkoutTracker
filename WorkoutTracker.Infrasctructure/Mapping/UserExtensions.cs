using WorkoutTracker.Core.Domain;
using WorkoutTracker.Infrasctructure.DTO;

namespace WorkoutTracker.Infrasctructure.Mapping
{
    public static class UserExtensions
    {
        public static UserReadDto MapToDto(this User user)
        {
            return new UserReadDto
            {
                Id = user.Id,
                UserName = user.UserName,
            };
        }

        //public static User MapToUser(this UserCreateDto userCreateDto)
        //{
        //    return new User(userCreateDto.UserName);
        //}

        public static User MapToUser(this UserRegisterDto userRegisterDto)
        {
            return new User(userRegisterDto.UserName, userRegisterDto.Email,
                userRegisterDto.Password, userRegisterDto.RoleId);
        }
    }
}
