using CourseAppCourseService_Domain;
using MongoDB.Driver;

namespace CourseAppCourseService_Application.Interfaces;

public interface ICourseDbContext
{
    IMongoDatabase Database { get; }
    IMongoCollection<Course> Courses { get; }
    IMongoCollection<Lesson> Lessons { get; }
    IMongoCollection<Quiz> Quizzes { get; }
    IMongoCollection<T> GetCollection<T>(string collectionName);
}