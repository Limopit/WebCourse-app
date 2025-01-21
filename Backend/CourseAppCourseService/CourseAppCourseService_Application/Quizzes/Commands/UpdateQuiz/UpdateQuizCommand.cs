using MediatR;

namespace CourseAppCourseService_Application.Quizzes.Commands.UpdateQuiz;

public record UpdateQuizCommand: IRequest
{
    public Guid Id { get; set; }
    public string Question {get; set;}
    public List<string> Options {get; set;}
    public string Answer {get; set;}
}