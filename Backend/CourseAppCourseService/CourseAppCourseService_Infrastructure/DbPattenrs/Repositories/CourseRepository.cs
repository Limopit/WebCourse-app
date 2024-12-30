using CourseAppCourseService_Application.Interfaces.Repositories;
using CourseAppCourseService_Domain;
using MongoDB.Driver;

namespace CourseAppCourseService_Infrastructure.DbPattenrs.Repositories;

public class CourseRepository(IMongoDatabase database, string collectionName)
    : BaseRepository<Course>(database, collectionName), ICourseRepository;