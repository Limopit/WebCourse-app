namespace CourseAppUserService_Application.UserCreatedCourse.Queries.GetUserCreatedCourses;

public record UserCreatedCourseDto
{
    public required string Id { get; set; }
    public required string ApprovementStatus { get; set; }
    public required DateTime ApprovementDate { get; set; }
}