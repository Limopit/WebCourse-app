using MediatR;

namespace CourseAppUserService_Application.UserCreatedCourse.Queries.GetUserCreatedCourses;

public class GetUserCreatedCoursesQuery: IRequest<UserCreatedCourseVm>
{
    public required string Email { get; set; }
}