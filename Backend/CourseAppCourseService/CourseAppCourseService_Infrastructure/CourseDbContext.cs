using CourseAppCourseService_Application.Interfaces;
using CourseAppCourseService_Domain;
using MongoDB.Driver;

namespace CourseAppCourseService_Infrastructure;

public class CourseDbContext : ICourseDbContext
{
    public IMongoDatabase Database { get; }
    public IMongoCollection<Course> Courses { get; }
    public IMongoCollection<Lesson> Lessons { get; }
    public IMongoCollection<Quiz> Quizzes { get; }

    public CourseDbContext(IMongoClient mongoClient, string databaseName)
    {
        Database = mongoClient.GetDatabase(databaseName);
        
        Courses = Database.GetCollection<Course>("Courses");
        Lessons = Database.GetCollection<Lesson>("Lessons");
        Quizzes = Database.GetCollection<Quiz>("Quizzes");
    }

    public IMongoCollection<T> GetCollection<T>(string collectionName)
    {
        return Database.GetCollection<T>(collectionName);
    }
}
