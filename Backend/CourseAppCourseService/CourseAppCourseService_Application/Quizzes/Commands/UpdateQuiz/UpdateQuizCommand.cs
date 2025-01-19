using MediatR;

namespace CourseAppCourseService_Application.Quizzes.Commands.UpdateQuiz;

public record UpdateQuizCommand: IRequest
{
    public Guid Id { get; set; }
    public string QuizQuestion {get; set;}
    public List<string> QuizOptions {get; set;}
    public string QuizAnswer {get; set;}
}