using CourseAppCourseService_Application.Interfaces;
using CourseAppCourseService_Application.Interfaces.Repositories;
using CourseAppCourseService_Domain;
using MongoDB.Driver;

namespace CourseAppCourseService_Infrastructure.DbPattenrs.Repositories;

public class CourseRepository(ICourseDbContext context)
    : BaseRepository<Course>(context, "Courses"), ICourseRepository
{
    
}