using CourseAppUserService_Application.UserTakenCourse.Commands.CreateUserTakenCourse;
using FluentValidation;

namespace CourseAppUserService_Application.UserCreatedCourse.Commands.CreateUserCreatedCourse;

public class CreateUserCreatedCourseCommandValidator: AbstractValidator<CreateUserCreatedCourseCommand>
{
    public CreateUserCreatedCourseCommandValidator()
    {
        RuleFor(command => command.ApprovementStatus).NotEmpty().MaximumLength(15)
            .WithMessage("Status must be shorter than 15 characters");
    }
}