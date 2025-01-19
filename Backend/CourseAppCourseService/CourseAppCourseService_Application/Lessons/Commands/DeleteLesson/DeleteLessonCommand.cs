using MediatR;

namespace CourseAppCourseService_Application.Lessons.Commands.DeleteLesson;

public record DeleteLessonCommand: IRequest
{
    public Guid LessonId { get; set; }
}