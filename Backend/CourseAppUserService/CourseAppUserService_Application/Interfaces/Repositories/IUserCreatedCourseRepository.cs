using CourseAppUserService_Domain.Entities;
using CourseAppUserService_Domain.Enums;

namespace CourseAppUserService_Application.Interfaces.Repositories;

public interface IUserCreatedCourseRepository: IBaseRepository<UserCreatedCourses>
{
    Task<List<UserCreatedCourses>> GetUserCreatedCoursesAsync(string userId, CancellationToken token);
    Task<List<string>> GetUserCoursesAsync(ApprovementStatus status, CancellationToken token);
    Task<UserCreatedCourses?> GetUserCreatedCourseByCourseIdAsync(string courseId, CancellationToken token);
}