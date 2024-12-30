using CourseAppUserService_Domain.Entities;

namespace CourseAppUserService_Application.Interfaces.Repositories;

public interface IUserTakenCourseRepository: IBaseRepository<UserTakenCourses>
{
    Task<List<UserTakenCourses>> GetUserTakenCoursesAsync(string userId, CancellationToken token);
}