using System.ComponentModel.DataAnnotations;
using WorkoutTracker.Core.Domain;

namespace WorkoutTracker.Infrasctructure.DTO
{
    public class UserRegisterDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public int RoleId { get; set; } = 1;
    }
}
