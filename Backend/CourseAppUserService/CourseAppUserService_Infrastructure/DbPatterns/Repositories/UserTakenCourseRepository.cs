using CourseAppUserService_Application.Interfaces.Repositories;
using CourseAppUserService_Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CourseAppUserService_Persistance.DbPatterns.Repositories;

public class UserTakenCourseRepository(UserServiceDbContext context)
    : BaseRepository<UserTakenCourses>(context), IUserTakenCourseRepository
{
    public async Task<List<UserTakenCourses>> GetUserTakenCoursesAsync(string userId, CancellationToken token)
    {
        return await context.UserTakenCourses
            .Where(course => course.UserId == userId)
            .ToListAsync(token);
    }

    public async Task<List<UserTakenCourses>> GetEachUserTakenCoursesByCourseIdAsync(string courseId, CancellationToken token)
    {
        return await context.UserTakenCourses
            .Where(course => course.CourseId == courseId)
            .ToListAsync(token);
    }

    public async Task<UserTakenCourses> GetUserTakenCoursesByCourseIdAsync(string courseId, User user, CancellationToken token)
    {
        return await context.UserTakenCourses
            .FirstOrDefaultAsync(course => course.CourseId == courseId && course.User.Equals(user), token);
    }
}