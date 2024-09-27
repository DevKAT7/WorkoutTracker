using FluentValidation;
using WorkoutTracker.Infrasctructure.Data;

namespace WorkoutTracker.Infrasctructure.DTO.Validators
{
    public class UserRegisterDtoValidator : AbstractValidator<UserRegisterDto>
    {
        public UserRegisterDtoValidator(WorkoutTrackerContext dbContext)
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Password).MinimumLength(8);

            RuleFor(x => x.ConfirmPassword).Equal(e => e.Password);

            RuleFor(x => x.UserName).NotEmpty();

            RuleFor(x => x.Email)
                .Custom((value, context) =>
                {
                    var emailInUse = dbContext.Users.Any(u => u.Email == value);

                    if (emailInUse)
                    {
                        context.AddFailure("Email", "That email is taken.");
                    }
                });

            RuleFor(x => x.UserName)
                .Custom((value, context) =>
                {
                    var userNameInUse = dbContext.Users.Any(u => u.UserName == value);

                    if (userNameInUse)
                    {
                        context.AddFailure("UserName", "That username is taken.");
                    }
                });
        }
    }
}
