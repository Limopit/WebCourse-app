using FluentValidation;

namespace CourseAppUserService_Application.UserTakenCourse.Commands.CreateUserTakenCourse;

public class CreateUserTakenCourseCommandValidator: AbstractValidator<CreateUserTakenCourseCommand>
{
    public CreateUserTakenCourseCommandValidator()
    {
        RuleFor(command => command.Status).NotEmpty().MaximumLength(15)
            .WithMessage("Status must be shorter than 15 characters");
        RuleFor(command => command.StartDate).NotEmpty().LessThanOrEqualTo(DateTime.Now)
            .WithMessage("Course can not be started in future");
    }
}