using CourseAppUserService_Domain.Entities;

namespace CourseAppUserService_Application.Interfaces.Repositories;

public interface IUserTakenCourseRepository: IBaseRepository<UserTakenCourses>
{
    Task<List<UserTakenCourses>> GetUserTakenCoursesAsync(string userId, CancellationToken token);
    Task<List<UserTakenCourses>> GetEachUserTakenCoursesByCourseIdAsync(string courseId, CancellationToken token);
    Task<UserTakenCourses> GetUserTakenCoursesByCourseIdAsync(string courseId, User user, CancellationToken token);
}