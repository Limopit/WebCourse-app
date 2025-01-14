using MediatR;

namespace CourseAppCourseService_Application.Quizzes.Commands.DeleteQuiz;

public class DeleteQuizCommand: IRequest
{
    public Guid QuizId { get; set; }
}