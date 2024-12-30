using CourseAppUserService_Domain.Entities;

namespace CourseAppUserService_Application.Interfaces.Repositories;

public interface IUserCreatedCourseRepository: IBaseRepository<UserCreatedCourses>
{
    Task<List<UserCreatedCourses>> GetUserCreatedCoursesAsync(string userId, CancellationToken token);
}