using MediatR;

namespace CourseAppUserService_Application.UserTakenCourse.Queries.GetUsersTakenCourses;

public class GetUsersTakenCoursesQuery: IRequest<UserTakenCourseVm>
{
    public string Email { get; set; }
}