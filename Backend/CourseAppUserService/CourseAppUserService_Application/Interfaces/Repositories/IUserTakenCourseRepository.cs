using CourseAppUserService_Domain;

namespace CourseAppUserService_Application.Interfaces.Repositories;

public interface IUserTakenCourseRepository: IBaseRepository<UserTakenCourses>
{
    Task<List<UserTakenCourses>> GetUserTakenCoursesAsync(string userId, CancellationToken token);
}