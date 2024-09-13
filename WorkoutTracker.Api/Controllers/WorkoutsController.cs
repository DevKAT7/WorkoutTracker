using Microsoft.AspNetCore.Mvc;
using WorkoutTracker.Core.Repositories;
using WorkoutTracker.Infrasctructure.DTO;
using WorkoutTracker.Infrasctructure.Mapping;

namespace WorkoutTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkoutsController : ControllerBase
    {
        private readonly IWorkoutRepository _workoutRepository;

        public WorkoutsController(IWorkoutRepository workoutRepository)
        {
            _workoutRepository = workoutRepository;
        }

        //GET api/workouts/user/{userId}
        [HttpGet("user/{userId}")]
        public ActionResult<IEnumerable<WorkoutReadDto>> GetAllWorkouts(int userId)
        {
            var workouts = _workoutRepository.GetAll(userId);

            return Ok(workouts.Select(x => x.MapToDto()));
        }

        //GET api/workouts/{id}
        [HttpGet("{id}", Name = "GetWorkoutById")]
        public ActionResult<WorkoutReadDto> GetWorkoutById(int id)
        {
            var workout = _workoutRepository.Get(id);

            var workoutDto = workout.MapToDto();

            if (workoutDto != null)
            {
                return Ok(workoutDto);
            }

            return NotFound();
        }

        //POST api/workouts/
        [HttpPost]
        public ActionResult<WorkoutReadDto> AddWorkout(WorkoutCreateDto workoutCreateDto)
        {
            var workout = workoutCreateDto.MapToWorkout();

            _workoutRepository.Add(workout);

            _workoutRepository.SaveChanges();

            var workoutReadDto = workout.MapToDto();

            return CreatedAtRoute(nameof(GetWorkoutById), new { Id = workoutReadDto.Id }, workoutReadDto);
        }

        //PUT api/workouts/add/{workoutId}, {exerciseId}
        [HttpPut("add/{workoutId}, {exerciseId}")]
        public ActionResult AddExerciseToWorkout(int workoutId, int exerciseId)
        {
            var workout = _workoutRepository.Get(workoutId);

            if (workout == null)
            {
                return NotFound();
            }

            _workoutRepository.AddExerciseToWorkout(workoutId, exerciseId);

            _workoutRepository.SaveChanges();

            return NoContent();
        }

        //PUT api/workouts/{id}
        [HttpPut("delete/{workoutId}, {exerciseId}")]
        public ActionResult DeleteExerciseFromWorkout(int workoutId, int exerciseId)
        {
            var workout = _workoutRepository.Get(workoutId);

            if (workout == null)
            {
                return NotFound();
            }

            _workoutRepository.DeleteExerciseFromWorkout(workoutId, exerciseId);

            _workoutRepository.SaveChanges();

            return NoContent();
        }

        //PUT api/workouts/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateWorkout(int id, string name, DayOfWeek? day)
        {
            var workout = _workoutRepository.Get(id);

            if (workout == null)
            {
                return NotFound();
            }

            workout.Update(name, day);

            _workoutRepository.SaveChanges();

            return NoContent();
        }

        //DELETE api/workouts/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteWorkout(int id)
        {
            var workout = _workoutRepository.Get(id);

            if (workout == null)
            {
                return NotFound();
            }

            _workoutRepository.Delete(workout);

            _workoutRepository.SaveChanges();

            return NoContent();
        }
    }
}
