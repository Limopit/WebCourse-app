using MediatR;

namespace CourseAppCourseService_Application.Lessons.Commands.DeleteLesson;

public class DeleteLessonCommand: IRequest
{
    public Guid LessonId { get; set; }
}