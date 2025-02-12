using CourseAppCourseService_Domain;

namespace CourseAppCourseService_Application.Interfaces.Repositories;

public interface ICourseRepository : IBaseRepository<Course>
{
    public Task<Course?> GetCourseByIdWithLessonsAsync(Guid courseId, ILessonRepository lessonRepository, CancellationToken token);
}