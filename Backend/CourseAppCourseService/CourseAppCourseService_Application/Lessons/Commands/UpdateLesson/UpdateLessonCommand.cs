using MediatR;

namespace CourseAppCourseService_Application.Lessons.Commands.UpdateLesson;

public record UpdateLessonCommand: IRequest
{
    public Guid Id { get; set; }
    public string Title {get;set;}
    public string Description {get;set;}
    public int Duration {get;set;}
    public List<Guid> Quizzes {get;set;}
    public string Content {get;set;}
}