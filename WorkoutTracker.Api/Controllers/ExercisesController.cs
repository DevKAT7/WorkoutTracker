using Microsoft.AspNetCore.Mvc;
using WorkoutTracker.Core.Repositories;
using WorkoutTracker.Infrasctructure.DTO;
using WorkoutTracker.Infrasctructure.Mapping;

namespace WorkoutTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExercisesController : ControllerBase
    {
        private readonly IExerciseRepository _exerciseRepository;

        public ExercisesController(IExerciseRepository exerciseRepository)
        {
            _exerciseRepository = exerciseRepository;
        }

        //GET api/exercises/{id}
        [HttpGet("{id}", Name = "GetExerciseById")]
        public ActionResult<ExerciseReadDto> GetExerciseById(int id)
        {
            var exercise = _exerciseRepository.Get(id);

            var exerciseRedDto = exercise.MapToDto();

            if (exerciseRedDto != null)
            {
                return Ok(exerciseRedDto);
            }

            return NotFound();
        }

        //GET api/exercise/workout/{workoutId}
        [HttpGet("workout/{workoutId}")]
        public ActionResult<IEnumerable<ExerciseReadDto>> GetAllExercises(int workoutId)
        {
            var exercises = _exerciseRepository.GetAll(workoutId);

            return Ok(exercises.Select(x => x.MapToDto()));
        }

        //POST api/exercises
        //[HttpPost]
        //public ActionResult<ExerciseReadDto> AddExercise(ExerciseCreateDto exerciseCreateDto, int workoutId)
        //{
        //    var exercise = exerciseCreateDto.MapToExercise();

        //    _exerciseRepository.Add(exercise, workoutId);

        //    var exerciseReadDto = exercise.MapToDto();

        //    return CreatedAtRoute(nameof(GetExerciseById), new {Id = exerciseReadDto.Id}, exerciseReadDto);
        //}

        //POST api/exercises
        [HttpPost]
        public ActionResult<ExerciseReadDto> AddExercise(ExerciseCreateDto exerciseCreateDto)
        {
            var exercise = exerciseCreateDto.MapToExercise();

            _exerciseRepository.Add(exercise);

            _exerciseRepository.SaveChanges();

            var exerciseReadDto = exercise.MapToDto();

            return CreatedAtRoute(nameof(GetExerciseById), new { Id = exerciseReadDto.Id }, exerciseReadDto);
        }

        //PUT api/exercises/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateExercise(int id, string name, int? sets)
        {
            var exercise = _exerciseRepository.Get(id);

            if (exercise == null)
            {
                return NotFound();
            }

            exercise.Update(name, sets);

            _exerciseRepository.SaveChanges();

            return NoContent();
        }

        //DELETE api/exercises/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteWorkout(int id)
        {
            var exercise = _exerciseRepository.Get(id);

            if (exercise == null)
            {
                return NotFound();
            }

            _exerciseRepository.Delete(exercise);

            _exerciseRepository.SaveChanges();

            return NoContent();
        }
    }
}
