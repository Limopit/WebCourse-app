namespace CourseAppCourseService_Application.Lessons.Queries.GetLessonList;

public record LessonDto
{
    public Guid Id { get; set; }
    public string LessonTitle {get;set;}
    public string LessonDescription {get;set;}
}