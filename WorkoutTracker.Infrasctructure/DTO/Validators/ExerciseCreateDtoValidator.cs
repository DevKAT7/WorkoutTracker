using FluentValidation;
using WorkoutTracker.Infrasctructure.Data;

namespace WorkoutTracker.Infrasctructure.DTO.Validators
{
    public class ExerciseCreateDtoValidator : AbstractValidator<ExerciseCreateDto>
    {
        public ExerciseCreateDtoValidator(WorkoutTrackerContext dbContext)
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Exercise name is required.")
                .MinimumLength(3).WithMessage("Exercise name must be at least 3 characters long.");

            RuleFor(x => x.Sets)
                .GreaterThan(0).When(x => x.Sets.HasValue)
                .WithMessage("Sets must be greater than 0.");
        }
    }
}
