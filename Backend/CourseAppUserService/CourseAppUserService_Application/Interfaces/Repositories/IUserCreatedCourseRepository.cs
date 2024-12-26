using CourseAppUserService_Domain;

namespace CourseAppUserService_Application.Interfaces.Repositories;

public interface IUserCreatedCourseRepository: IBaseRepository<UserCreatedCourses>
{
    Task<List<UserCreatedCourses>> GetUserCreatedCoursesAsync(string userId, CancellationToken token);
}