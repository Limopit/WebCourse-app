using CourseAppCourseService_Application.Interfaces;
using CourseAppCourseService_Application.Interfaces.Repositories;
using MongoDB.Driver;
using CourseAppCourseService_Infrastructure.DbPattenrs.Repositories;

namespace CourseAppCourseService_Infrastructure.DbPattenrs
{
    public class UnitOfWork(ICourseDbContext context) : IUnitOfWork
    { 
        public ICourseRepository Courses { get; set; } = new CourseRepository(context);
        public ILessonRepository Lessons { get; set; } = new LessonRepository(context);
        public IQuizRepository Quizzes { get; set; } = new QuizRepository(context);
    }
}
