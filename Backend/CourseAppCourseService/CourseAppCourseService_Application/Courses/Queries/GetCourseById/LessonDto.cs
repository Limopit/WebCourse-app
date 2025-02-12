namespace CourseAppCourseService_Application.Courses.Queries.GetCourseById;

public class LessonDto
{
    public string Title {get;set;}
    public string Description {get;set;}
    public int Duration {get;set;}
    public List<QuizDto> QuizDetails {get;set;}
    public string Content {get;set;}
}