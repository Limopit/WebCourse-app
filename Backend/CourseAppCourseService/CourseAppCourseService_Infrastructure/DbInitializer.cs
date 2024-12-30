using CourseAppCourseService_Application.Interfaces;
using CourseAppCourseService_Domain;
using MongoDB.Driver;

namespace CourseAppCourseService_Infrastructure;

public class DbInitializer
{
    public static async Task Initialize(ICourseDbContext courseDbContext)
    {
        CreateCollectionIfNotExists("Courses", courseDbContext.Database);
        CreateCollectionIfNotExists("Lessons", courseDbContext.Database);
        CreateCollectionIfNotExists("Quizzes", courseDbContext.Database);

        await SeedDataAsync(courseDbContext.Courses, courseDbContext.Lessons, courseDbContext.Quizzes);
    }

    private static void CreateCollectionIfNotExists(string collectionName, IMongoDatabase database)
    {
        var collections = database.ListCollectionNames().ToList();
        if (!collections.Contains(collectionName))
        {
            database.CreateCollection(collectionName);
        }
    }

    private static async Task SeedDataAsync(IMongoCollection<Course> Courses, IMongoCollection<Lesson> Lessons, IMongoCollection<Quiz> Quizzes)
    {
        var coursesCount = await Courses.CountDocumentsAsync(FilterDefinition<Course>.Empty);
        if (coursesCount == 0)
        {
            var initialCourses = new List<Course>
            {
                new Course { CourseId = Guid.NewGuid().ToString(), Name = "C# Basics" },
                new Course { CourseId = Guid.NewGuid().ToString(), Name = "ASP.NET Core" }
            };
            await Courses.InsertManyAsync(initialCourses);
        }

        var lessonsCount = await Lessons.CountDocumentsAsync(FilterDefinition<Lesson>.Empty);
        if (lessonsCount == 0)
        {
            var initialLessons = new List<Lesson>
            {
                new Lesson { LessonId = Guid.NewGuid().ToString(), LessonName = "Lesson 1: Introduction to C#" },
                new Lesson { LessonId = Guid.NewGuid().ToString(), LessonName = "Lesson 2: ASP.NET Core Overview" }
            };
            await Lessons.InsertManyAsync(initialLessons);
        }

        var quizzesCount = await Quizzes.CountDocumentsAsync(FilterDefinition<Quiz>.Empty);
        if (quizzesCount == 0)
        {
            var initialQuizzes = new List<Quiz>
            {
                new Quiz { QuizId = Guid.NewGuid().ToString(), QuizName = "C# Quiz" },
                new Quiz { QuizId = Guid.NewGuid().ToString(), QuizName = "ASP.NET Core Quiz" }
            };
            await Quizzes.InsertManyAsync(initialQuizzes);
        }
    }
}
