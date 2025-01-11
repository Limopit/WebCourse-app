using CourseAppCourseService_Application.Interfaces;
using CourseAppCourseService_Domain;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CourseAppCourseService_Infrastructure;

public static class DbInitializer
{
    public static async Task Initialize(ICourseDbContext courseDbContext)
    {
        RefreshCollection("Courses", courseDbContext.Database);
        RefreshCollection("Lessons", courseDbContext.Database);
        RefreshCollection("Quizzes", courseDbContext.Database);

        await SeedDataAsync(courseDbContext.Courses, courseDbContext.Lessons, courseDbContext.Quizzes);
    }

    private static void RefreshCollection(string collectionName, IMongoDatabase database)
    {
        var collections = database.ListCollectionNames().ToList();
        if (!collections.Contains(collectionName))
        {
            database.CreateCollection(collectionName);
        }
        else
        {
            database.GetCollection<BsonDocument>(collectionName).DeleteMany(FilterDefinition<BsonDocument>.Empty);
        }
    }

    private static async Task SeedDataAsync(IMongoCollection<Course> Courses, IMongoCollection<Lesson> Lessons, IMongoCollection<Quiz> Quizzes)
    {
        var coursesCount = await Courses.CountDocumentsAsync(FilterDefinition<Course>.Empty);
        if (coursesCount == 0)
        {
            var initialCourses = new List<Course>
            {
                new Course { CourseTitle = "C# Basics" },
                new Course { CourseTitle = "ASP.NET Core" }
            };
            await Courses.InsertManyAsync(initialCourses);
        }

        var lessonsCount = await Lessons.CountDocumentsAsync(FilterDefinition<Lesson>.Empty);
        if (lessonsCount == 0)
        {
            var initialLessons = new List<Lesson>
            {
                new Lesson { LessonTitle = "Lesson 1: Introduction to C#" },
                new Lesson { LessonTitle = "Lesson 2: ASP.NET Core Overview" }
            };
            await Lessons.InsertManyAsync(initialLessons);
        }

        var quizzesCount = await Quizzes.CountDocumentsAsync(FilterDefinition<Quiz>.Empty);
        if (quizzesCount == 0)
        {
            var initialQuizzes = new List<Quiz>
            {
                new Quiz { QuizQuestion = "C# Quiz" },
                new Quiz { QuizQuestion = "ASP.NET Core Quiz" }
            };
            await Quizzes.InsertManyAsync(initialQuizzes);
        }
    }
}
