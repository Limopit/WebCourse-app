using FluentValidation;

namespace CourseAppCourseService_Application.Quizzes.Commands.CreateQuiz;

public class CreateQuizCommandValidator: AbstractValidator<CreateQuizCommand>
{
    public CreateQuizCommandValidator()
    {
        RuleFor(command => command.Question).NotEmpty().WithMessage("Question is required");
        RuleFor(command => command.Answer).NotEmpty().WithMessage("Answer is required");
        RuleFor(command => command.Options).NotEmpty().WithMessage("Options are required");
    }
}