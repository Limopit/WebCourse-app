namespace CourseAppCourseService_Application.Courses.Queries.GetCourseListInfo;

public record CourseVm
{
    public IList<CourseDto> Courses { get; set; }
}