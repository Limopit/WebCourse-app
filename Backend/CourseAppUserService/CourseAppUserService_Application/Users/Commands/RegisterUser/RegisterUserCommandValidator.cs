using FluentValidation;

namespace CourseAppUserService_Application.Users.Commands.RegisterUser;

public class RegisterUserCommandValidator: AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(command => command.FirstName).NotEmpty().MaximumLength(15)
            .WithMessage("Firstname is required shorter than 15 symb");
        RuleFor(command => command.LastName).NotEmpty().MaximumLength(25)
            .WithMessage("Lastname is required shorter than 25 symb");
    }
}