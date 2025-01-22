using CourseAppCourseService_Application.Interfaces.Repositories;

namespace CourseAppCourseService_Application.Interfaces;

public interface IUnitOfWork
{
    public ICourseRepository Courses { get; }
    public ILessonRepository Lessons { get; }
    public IQuizRepository Quizzes { get; }
}