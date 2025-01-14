using FluentValidation;

namespace CourseAppCourseService_Application.Quizzes.Commands.CreateQuiz;

public class CreateQuizCommandValidator: AbstractValidator<CreateQuizCommand>
{
    public CreateQuizCommandValidator()
    {
        RuleFor(command => command.QuizQuestion).NotEmpty().WithMessage("Question is required");
        RuleFor(command => command.QuizAnswer).NotEmpty().WithMessage("Answer is required");
        RuleFor(command => command.QuizOptions).NotEmpty().WithMessage("Options are required");
    }
}