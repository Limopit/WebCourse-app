namespace CourseAppCourseService_Application.Courses.Queries.GetCourseList;

public record CourseDto
{
    public Guid Id { get; set; }
    public string Title {get; set;}
    public string Description {get; set;}
    public string Logo {get; set;}
    public string Creator {get; set;}
}