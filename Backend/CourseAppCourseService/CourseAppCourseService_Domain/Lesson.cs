namespace CourseAppCourseService_Domain;

public class Lesson
{
    public Guid Id { get; set; }
    public string LessonTitle {get;set;}
    public string LessonDescription {get;set;}
    public int LessonDuration {get;set;}
    public string LessonType {get;set;}
    public string LessonContent {get;set;}
}