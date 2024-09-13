using Microsoft.AspNetCore.Mvc;
using WorkoutTracker.Core.Repositories;
using WorkoutTracker.Infrasctructure.DTO;
using WorkoutTracker.Infrasctructure.Mapping;

namespace WorkoutTracker.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
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

        //POST api/users
        [HttpPost]
        public ActionResult<UserReadDto> AddUser(UserCreateDto userCreateDto)
        {
            var user = userCreateDto.MapToUser();

            _userRepository.Add(user);

            _userRepository.SaveChanges();

            var userReadDto = user.MapToDto();

            return CreatedAtRoute(nameof(GetUserById), new { Id = userReadDto.Id }, userReadDto);
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
    }
}
