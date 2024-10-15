using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkoutTracker.Infrasctructure.DTO;
using WorkoutTracker.Infrasctructure.Services;

namespace WorkoutTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WorkoutsController : ControllerBase
    {
        private readonly IWorkoutService _workoutService;

        public WorkoutsController(IWorkoutService workoutService)
        {
            _workoutService = workoutService;
        }

        //GET api/workouts/user/{userId}
        [HttpGet("user/{userId}")]
        public ActionResult<IEnumerable<WorkoutReadDto>> GetAllWorkouts(int userId)
        {
            return Ok(_workoutService.GetAllWorkouts(userId));
        }

        //GET api/workouts/{id}
        [HttpGet("{id}", Name = "GetWorkoutById")]
        public ActionResult<WorkoutReadDto> GetWorkoutById(int id)
        {
            return Ok(_workoutService.GetWorkoutById(id));
        }

        //POST api/workouts/
        [HttpPost]
        public ActionResult<WorkoutReadDto> AddWorkout(WorkoutCreateDto workoutCreateDto)
        {
            var workoutReadDto = _workoutService.AddWorkout(workoutCreateDto);

            return CreatedAtRoute(nameof(GetWorkoutById), new { Id = workoutReadDto.Id }, workoutReadDto);
        }

        //PUT api/workouts/addExercise/{workoutId}, {exerciseId}
        [HttpPut("addExercise/{workoutId}, {exerciseId}")]
        public ActionResult AddExerciseToWorkout(int workoutId, int exerciseId)
        {
            _workoutService.AddExerciseToWorkout(workoutId, exerciseId);

            return NoContent();
        }

        //PUT api/workouts/deleteExercise/{workoutId}, {exerciseId}
        [HttpPut("deleteExercise/{workoutId}, {exerciseId}")]
        public ActionResult DeleteExerciseFromWorkout(int workoutId, int exerciseId)
        {
            _workoutService.DeleteExerciseFromWorkout(workoutId, exerciseId);

            return NoContent();
        }

        //PUT api/workouts/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateWorkout(int id, string name, DayOfWeek? day)
        {
            _workoutService.UpdateWorkout(id, name, day);

            return NoContent();
        }

        //DELETE api/workouts/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteWorkout(int id)
        {
            _workoutService.DeleteWorkout(id);

            return NoContent();
        }
    }
}
