using MediatR;

namespace CourseAppCourseService_Application.Lessons.Commands.CreateLesson;

public record CreateLessonCommand: IRequest<Guid>
{
    public string Title {get;set;}
    public string Description {get;set;}
    public int Duration {get;set;}
    public List<Guid> Quizzes {get;set;}
    public string Content {get;set;}
}