namespace CourseAppCourseService_Application.Lessons.Queries.GetLessonList;

public record LessonVm
{
    public IList<LessonDto> Lessons { get; set; }
}