using MediatR;

namespace CourseAppUserService_Application.UserCreatedCourse.Commands.DeleteUserCreatedCourse;

public record DeleteUserCreatedCourseCommand: IRequest
{
    public required string Id { get; set; }
}