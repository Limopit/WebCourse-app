using CourseAppCourseService_Application.Interfaces.Repositories;

namespace CourseAppCourseService_Application.Interfaces;

public interface IUnitOfWork
{
    public ICourseRepository Courses { get; set; }
    public ILessonRepository Lessons { get; set; }
    public IQuizRepository Quizzes { get; set; }
}