using Microsoft.AspNetCore.Mvc;
using WorkoutTracker.Infrasctructure.DTO;
using WorkoutTracker.Infrasctructure.Services;

namespace WorkoutTracker.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        //GET api/users
        [HttpGet]
        public ActionResult<IEnumerable<UserReadDto>> GetAllUsers()
        {
            return Ok(_userService.GetAllUsers());
        }

        //GET api/users/{id}
        [HttpGet("{id}", Name = "GetUserById")]
        public ActionResult<UserReadDto> GetUserById(int id)
        {
            var user = _userService.GetUserById(id);

            return Ok(user);
        }

        //PUT api/users/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateUser(int id, string userName, string email)
        {
            _userService.UpdateUser(id, userName, email);

            return NoContent();
        }

        //DELETE api/users/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteUser(int id)
        {
            _userService.DeleteUser(id);

            return NoContent();
        }

        //POST api/users/register
        [HttpPost("register")]
        public ActionResult RegisterUser(UserRegisterDto userRegisterDto)
        {
            _userService.RegisterUser(userRegisterDto);

            return Ok();
        }

        //POST api/users/login
        [HttpPost("login")]
        public ActionResult LoginUser(UserLoginDto userLoginDto)
        {
            string token = _userService.GenerateJwt(userLoginDto);

            return Ok(token);
        }
    }
}
