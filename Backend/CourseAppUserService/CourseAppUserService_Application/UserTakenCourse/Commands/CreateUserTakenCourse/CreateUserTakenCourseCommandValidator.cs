using FluentValidation;

namespace CourseAppUserService_Application.UserTakenCourse.Commands.CreateUserTakenCourse;

public class CreateUserTakenCourseCommandValidator: AbstractValidator<CreateUserTakenCourseCommand>
{
    public CreateUserTakenCourseCommandValidator()
    {
        RuleFor(command => command.StartDate).NotEmpty().LessThanOrEqualTo(DateTime.Now)
            .WithMessage("Course can not be started in future");
    }
}