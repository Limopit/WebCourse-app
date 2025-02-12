using CourseAppCourseService_Application.Interfaces;
using CourseAppCourseService_Application.Interfaces.Repositories;
using CourseAppCourseService_Domain;
using MongoDB.Driver;

namespace CourseAppCourseService_Infrastructure.DbPattenrs.Repositories;

public class LessonRepository(ICourseDbContext context)
    : BaseRepository<Lesson>(context, "Lessons"), ILessonRepository
{
    public async Task<Lesson?> GetLessonByIdWithQuizzesAsync(Guid lessonId, CancellationToken cancellationToken)
    {
        var lesson = await _collection.Find(Builders<Lesson>.Filter.Eq("_id", lessonId)).FirstOrDefaultAsync(cancellationToken);
        if (lesson == null)
        {
            return null;
        }

        var quizIds = lesson.Quizzes.Select(x => x.ToString()).ToList();
        var quizFilter = Builders<Quiz>.Filter.In("_id", quizIds);
        
        var quizzes = await context.Database.GetCollection<Quiz>("Quizzes")
            .Find(quizFilter)
            .ToListAsync(cancellationToken);

        lesson.QuizDetails = quizzes;

        return lesson;
    }
}