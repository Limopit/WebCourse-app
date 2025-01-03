using FluentValidation;

namespace CourseAppCourseService_Application.Courses.Commands.CreateCourse;

public class CreateCourseCommandValidator: AbstractValidator<CreateCourseCommand>
{
    public CreateCourseCommandValidator()
    {
        RuleFor(command => command.CourseTitle).NotEmpty().MaximumLength(20).WithMessage("Course title must be between 1 and 20 characters.");
        RuleFor(command => command.CourseDescription).NotEmpty().WithMessage("Course description is required.");
        RuleFor(command => command.CourseLanguage).NotEmpty().WithMessage("Course language is required.");
    }
}