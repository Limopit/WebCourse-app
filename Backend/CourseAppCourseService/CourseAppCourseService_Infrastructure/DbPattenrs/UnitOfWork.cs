using CourseAppCourseService_Application.Interfaces;
using CourseAppCourseService_Application.Interfaces.Repositories;

namespace CourseAppCourseService_Infrastructure.DbPattenrs;

public class UnitOfWork(
    ICourseRepository courseRepository,
    ILessonRepository lessonRepository,
    IQuizRepository quizRepository)
    : IUnitOfWork
{
    public ICourseRepository Courses { get; } = courseRepository;
    public ILessonRepository Lessons { get; } = lessonRepository;
    public IQuizRepository Quizzes { get; } = quizRepository;
}