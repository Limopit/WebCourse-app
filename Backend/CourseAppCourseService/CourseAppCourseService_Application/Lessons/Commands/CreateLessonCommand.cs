using MediatR;

namespace CourseAppCourseService_Application.Lessons.Commands;

public class CreateLessonCommand: IRequest<Guid>
{
    public string LessonTitle {get;set;}
    public string LessonDescription {get;set;}
    public int LessonDuration {get;set;}
    public string LessonType {get;set;}
    public string LessonContent {get;set;}
}