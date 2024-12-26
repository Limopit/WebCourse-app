namespace CourseAppUserService_Application.UserTakenCourse.Queries.GetUsersTakenCourses;

public class UserTakenCourseDto
{
    public required string CourseId { get; set; }
    public required string Status { get; set; }
    public required DateTime StartDate { get; set; }
    public DateTime FinishDate { get; set; }
    public bool IsFavorite { get; set; }
}