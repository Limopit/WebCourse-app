using CourseAppUserService_Domain;
using MediatR;

namespace CourseAppUserService_Application.UserCreatedCourse.Queries.GetUserCreatedCourses;

public class GetUserCreatedCoursesQuery: IRequest<UserCreatedCourseVm>
{
    public string Email { get; set; }
}