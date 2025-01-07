using FluentValidation;

namespace CourseAppCourseService_Application.Lessons.Commands.CreateLesson;

public class CreateLessonCommandValidator: AbstractValidator<CreateLessonCommand>
{
    public CreateLessonCommandValidator()
    {
        RuleFor(command => command.LessonTitle).NotEmpty().MaximumLength(20).WithMessage("Title must be between 1 and 20 characters.");
        RuleFor(command => command.LessonDescription).NotEmpty().WithMessage("Description is required.");
    }
}