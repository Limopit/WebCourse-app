using MediatR;

namespace CourseAppCourseService_Application.Quizzes.Commands.DeleteQuiz;

public record DeleteQuizCommand: IRequest
{
    public Guid QuizId { get; set; }
}