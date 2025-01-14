using CourseAppUserService_Domain.Entities;

namespace CourseAppUserService_Application.Interfaces.Repositories;

public interface IUserTakenCourseRepository: IBaseRepository<UserTakenCourses>
{
    Task<List<UserTakenCourses>> GetUserTakenCoursesAsync(string userId, CancellationToken token);
    Task<List<UserTakenCourses>> GetUserTakenCoursesByCourseIdAsync(string courseId, CancellationToken token);
}