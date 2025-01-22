using MediatR;

namespace CourseAppCourseService_Application.Courses.Commands.DeleteCourse;

public record DeleteCourseCommand: IRequest
{
    public Guid Id { get; set; }
}