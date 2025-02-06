using CourseAppCourseService_Application.Interfaces;
using CourseAppCourseService_Application.Interfaces.Repositories;
using CourseAppCourseService_Domain;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CourseAppCourseService_Infrastructure.DbPattenrs.Repositories;

public class CourseRepository(ICourseDbContext context)
    : BaseRepository<Course>(context, "Courses"), ICourseRepository
{
    public async Task<Course?> GetCourseByIdWithLessonsAsync(Guid courseId, CancellationToken token)
    {
        var course = await _collection.Find(Builders<Course>.Filter.Eq("_id", courseId)).FirstOrDefaultAsync(token);
        if (course == null)
        {
            return null;
        }

        var lessonIds = course.Lessons.Select(x => x.ToString()).ToList();
        var lessonsFilter = Builders<Lesson>.Filter.In("_id", lessonIds);
        
        var lessons = await context.Database.GetCollection<Lesson>("Lessons")
            .Find(lessonsFilter)
            .ToListAsync(token);

        course.LessonDetails = lessons;

        return course;
    }

}