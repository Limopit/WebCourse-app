namespace CourseAppCourseService_Application.Courses.Queries.GetCourseById;

public class QuizDto
{
    public string Question {get; set;}
    public List<string> Options {get; set;}
    public string Answer {get; set;}
}