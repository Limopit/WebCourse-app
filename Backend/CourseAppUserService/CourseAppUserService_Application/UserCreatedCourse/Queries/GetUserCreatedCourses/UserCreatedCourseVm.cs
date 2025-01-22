namespace CourseAppUserService_Application.UserCreatedCourse.Queries.GetUserCreatedCourses;

public record UserCreatedCourseVm
{
    public IList<UserCreatedCourseDto> UserCreatedCourses { get; set; }
}