namespace CourseAppUserService_Application.UserTakenCourse.Queries.GetUsersTakenCourses;

public record UserTakenCourseDto
{
    public required string Id { get; set; }
    public required string Status { get; set; }
    public required DateTime StartDate { get; set; }
    public DateTime FinishDate { get; set; }
    public bool IsFavorite { get; set; }
}