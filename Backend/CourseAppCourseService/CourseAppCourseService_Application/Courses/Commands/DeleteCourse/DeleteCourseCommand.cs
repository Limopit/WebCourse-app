using MediatR;

namespace CourseAppCourseService_Application.Courses.Commands.DeleteCourse;

public class DeleteCourseCommand: IRequest
{
    public Guid Id { get; set; }
}