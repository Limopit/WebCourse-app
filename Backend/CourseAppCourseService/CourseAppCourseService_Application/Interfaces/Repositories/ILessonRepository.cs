using CourseAppCourseService_Domain;

namespace CourseAppCourseService_Application.Interfaces.Repositories;

public interface ILessonRepository : IBaseRepository<Lesson>
{
    Task<Lesson?> GetLessonByIdWithQuizzesAsync(Guid id, CancellationToken cancellationToken);
}