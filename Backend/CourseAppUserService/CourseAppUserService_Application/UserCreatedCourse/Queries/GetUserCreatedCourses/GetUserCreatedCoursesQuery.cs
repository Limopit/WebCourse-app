using MediatR;

namespace CourseAppUserService_Application.UserCreatedCourse.Queries.GetUserCreatedCourses;

public record GetUserCreatedCoursesQuery: IRequest<UserCreatedCourseVm>
{
    public required string Email { get; set; }
}