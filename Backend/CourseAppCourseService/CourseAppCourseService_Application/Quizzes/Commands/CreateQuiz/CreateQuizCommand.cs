using MediatR;

namespace CourseAppCourseService_Application.Quizzes.Commands.CreateQuiz;

public record CreateQuizCommand: IRequest<Guid>
{
    public string Question {get; set;}
    public List<string> Options {get; set;}
    public string Answer {get; set;}
}