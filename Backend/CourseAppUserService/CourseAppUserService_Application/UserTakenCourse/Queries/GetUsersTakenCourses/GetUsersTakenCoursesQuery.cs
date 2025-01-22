using MediatR;

namespace CourseAppUserService_Application.UserTakenCourse.Queries.GetUsersTakenCourses;

public record GetUsersTakenCoursesQuery: IRequest<UserTakenCourseVm>
{
    public required string Email { get; set; }
}