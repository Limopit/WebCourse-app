using MediatR;

namespace CourseAppCourseService_Application.Courses.Commands.CreateCourse;

public record CreateCourseCommand: IRequest<Guid>
{
    public string Title {get; set;}
    public string Description {get; set;}
    public string Logo {get; set;}
    public string Level {get; set;}
    public string Category {get; set;}
    public string Language {get; set;}
    public string Requierments {get; set;}
}