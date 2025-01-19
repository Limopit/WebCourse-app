namespace CourseAppUserService_Application.UserTakenCourse.Queries.GetUsersTakenCourses;

public record UserTakenCourseVm
{
    public IList<UserTakenCourseDto> UserTakenCourses { get; set; }
}