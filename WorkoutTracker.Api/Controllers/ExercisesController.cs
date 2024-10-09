using Microsoft.AspNetCore.Mvc;
using WorkoutTracker.Infrasctructure.DTO;
using WorkoutTracker.Infrasctructure.Services;

namespace WorkoutTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExercisesController : ControllerBase
    {
        private readonly IExerciseService _exerciseService;

        public ExercisesController(IExerciseService exerciseService)
        {
            _exerciseService = exerciseService;
        }

        //GET api/exercises/{id}
        [HttpGet("{id}", Name = "GetExerciseById")]
        public ActionResult<ExerciseReadDto> GetExerciseById(int id)
        {
            return Ok(_exerciseService.GetExerciseById(id));
        }

        //GET api/exercise/workout/{workoutId}
        [HttpGet("workout/{workoutId}")]
        public ActionResult<IEnumerable<ExerciseReadDto>> GetAllExercises(int workoutId)
        {
            return Ok(_exerciseService.GetAllExercises(workoutId));
        }

        //POST api/exercises
        [HttpPost]
        public ActionResult<ExerciseReadDto> AddExercise(ExerciseCreateDto exerciseCreateDto)
        {
            var exerciseReadDto = _exerciseService.AddExercise(exerciseCreateDto);

            return CreatedAtRoute(nameof(GetExerciseById), new { Id = exerciseReadDto.Id }, exerciseReadDto);
        }

        //PUT api/exercises/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateExercise(int id, string name, int? sets)
        {
            _exerciseService.UpdateExercise(id, name, sets);

            return NoContent();
        }

        //DELETE api/exercises/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteWorkout(int id)
        {
            _exerciseService.DeleteWorkout(id);

            return NoContent();
        }
    }
}
