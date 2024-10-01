using Microsoft.AspNetCore.Mvc;
using WorkoutTracker.Core.Repositories;
using WorkoutTracker.Infrasctructure.DTO;
using WorkoutTracker.Infrasctructure.Mapping;
using WorkoutTracker.Infrasctructure.Services;

namespace WorkoutTracker.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserService _accountService;

        public UsersController(IUserRepository userRepository, IUserService accountService)
        {
            _userRepository = userRepository;
            _accountService = accountService;
        }

        //GET api/users
        [HttpGet]
        public ActionResult<IEnumerable<UserReadDto>> GetAllUsers()
        {
            var users = _userRepository.GetAll();

            return Ok(users.Select(x => x.MapToDto()));
        }

        //GET api/users/{id}
        [HttpGet("{id}", Name = "GetUserById")]
        public ActionResult<UserReadDto> GetUserById(int id)
        {
            var user = _userRepository.Get(id);

            var userDto = user.MapToDto();

            if (userDto != null)
            {
                return Ok(userDto);
            }

            return NotFound();
        }

        //PUT api/users/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateUser(int id, string userName)
        {
            var user = _userRepository.Get(id);

            if (user == null)
            {
                return NotFound();
            }

            user.UpdateUser(userName);

            _userRepository.SaveChanges();

            return NoContent();
        }

        //DELETE api/users/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteUser(int id)
        {
            var user = _userRepository.Get(id);

            if (user == null)
            {
                return NotFound();
            }

            _userRepository.Delete(user);

            _userRepository.SaveChanges();

            return NoContent();
        }

        //POST api/users/register
        [HttpPost("register")]
        public ActionResult RegisterUser(UserRegisterDto userRegisterDto)
        {
            _accountService.RegisterUser(userRegisterDto);

            return Ok();
        }

        //POST api/users/login
        [HttpPost("login")]
        public ActionResult LoginUser(UserLoginDto userLoginDto)
        {
            string token = _accountService.GenerateJwt(userLoginDto);

            return Ok(token);
        }
    }
}
