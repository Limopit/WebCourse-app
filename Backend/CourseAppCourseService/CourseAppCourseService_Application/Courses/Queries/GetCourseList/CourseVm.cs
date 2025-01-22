namespace CourseAppCourseService_Application.Courses.Queries.GetCourseList;

public record CourseVm
{
    public IList<CourseDto> Courses { get; set; }
}