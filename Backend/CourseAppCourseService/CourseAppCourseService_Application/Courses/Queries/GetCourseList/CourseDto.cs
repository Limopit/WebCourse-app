namespace CourseAppCourseService_Application.Courses.Queries.GetCourseList;

public class CourseDto
{
    public Guid Id { get; set; }
    public string CourseTitle {get; set;}
    public string CourseDescription {get; set;}
    public string CourseLogo {get; set;}
    public string CourseCreator {get; set;}
}