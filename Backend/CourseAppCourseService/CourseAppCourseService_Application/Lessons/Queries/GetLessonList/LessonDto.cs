namespace CourseAppCourseService_Application.Lessons.Queries.GetLessonList;

public record LessonDto
{
    public Guid Id { get; set; }
    public string Title {get;set;}
    public string Description {get;set;}
}