using MediatR;

namespace CourseAppCourseService_Application.Courses.Commands.UpdateCourse;

public record UpdateCourseCommand: IRequest
{
    public Guid Id { get; set; }
    public string Title {get; set;}
    public string Description {get; set;}
    public string Logo {get; set;}
    public string Level {get; set;}
    public string Category {get; set;}
    public string Language {get; set;}
    public string Requierments {get; set;}
}