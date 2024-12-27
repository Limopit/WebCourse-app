using CourseAppUserService_Domain;
using FluentValidation;

namespace CourseAppUserService_Application.UserCreatedCourse.Commands.CreateUserCreatedCourse;

public class CreateUserCreatedCourseCommandValidator: AbstractValidator<CreateUserCreatedCourseCommand>
{
    public CreateUserCreatedCourseCommandValidator()
    {
        RuleFor(command => command.ApprovementStatus).Must(status => Enum.IsDefined(typeof(ApprovementStatus), status))
            .WithMessage("Invalid status value.");
    }
}