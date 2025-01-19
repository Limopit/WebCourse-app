using MediatR;

namespace CourseAppCourseService_Application.Courses.Commands.CreateCourse;

public record CreateCourseCommand: IRequest<Guid>
{
    public string CourseTitle {get; set;}
    public string CourseDescription {get; set;}
    public string CourseLogo {get; set;}
    public string CourseLevel {get; set;}
    public string CourseCategory {get; set;}
    public string CourseLanguage {get; set;}
    public string CourseRequierments {get; set;}
}