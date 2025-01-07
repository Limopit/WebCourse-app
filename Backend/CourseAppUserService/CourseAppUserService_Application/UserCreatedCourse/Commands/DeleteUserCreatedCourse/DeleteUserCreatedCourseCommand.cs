using MediatR;

namespace CourseAppUserService_Application.UserCreatedCourse.Commands.DeleteUserCreatedCourse;

public class DeleteUserCreatedCourseCommand: IRequest
{
    public string CourseId { get; set; }
}