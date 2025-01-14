using MediatR;

namespace CourseAppCourseService_Application.Quizzes.Commands.CreateQuiz;

public class CreateQuizCommand: IRequest<Guid>
{
    public string QuizQuestion {get; set;}
    public List<string> QuizOptions {get; set;}
    public string QuizAnswer {get; set;}
}