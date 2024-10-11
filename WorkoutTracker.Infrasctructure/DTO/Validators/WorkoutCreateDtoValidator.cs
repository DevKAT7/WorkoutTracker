using FluentValidation;
using WorkoutTracker.Infrasctructure.Data;

namespace WorkoutTracker.Infrasctructure.DTO.Validators
{
    public class WorkoutCreateDtoValidator : AbstractValidator<WorkoutCreateDto>
    {
        public WorkoutCreateDtoValidator(WorkoutTrackerContext dbContext)
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Workout name is required.")
                .MinimumLength(3).WithMessage("Workout name must be at least 3 characters long.");

            RuleFor(x => x.Day)
                .IsInEnum().WithMessage("Invalid day of the week.");

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User id is required.");

            RuleFor(x => x.UserId)
                .Custom((value, context) => 
                {
                    var userExists = dbContext.Users.Any(u => u.Id == value);

                    if (!userExists)
                    {
                        context.AddFailure("UserId", "Invalid user id.");
                    }
                });  
        }
    }
}
