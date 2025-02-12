using CourseAppCourseService_Domain;

namespace CourseAppCourseService_Application.Courses.Queries.GetCourseById;

public class CourseDto
{
    public string Id { get; set; }
    public string Title {get; set;}
    public string Description {get; set;}
    public string Logo {get; set;}
    public string Level {get; set;}
    public string Category {get; set;}
    public string Language {get; set;}
    public DateTime CreationDate {get; set;}
    public DateTime UpdateDate {get; set;}
    public List<LessonDto> LessonDetails {get; set;}
    public string Requierments {get; set;}
}