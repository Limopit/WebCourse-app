using MediatR;

namespace CourseAppUserService_Application.UserCreatedCourse.Commands.DeleteUserCreatedCourse;

public class DeleteUserCreatedCourseCommand: IRequest
{
    public required string CourseId { get; set; }
}