using CourseAppCourseService_Application.Interfaces;
using CourseAppCourseService_Application.Interfaces.Repositories;
using CourseAppCourseService_Domain;

namespace CourseAppCourseService_Infrastructure.DbPattenrs.Repositories;

public class LessonRepository(ICourseDbContext context)
    : BaseRepository<Lesson>(context, "Lessons"), ILessonRepository;