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

    public async Task<List<UserTakenCourses>> GetUserTakenCoursesByCourseIdAsync(string courseId, CancellationToken token)
    {
        return await context.UserTakenCourses
            .Where(course => course.CourseId == courseId)
            .ToListAsync(token);
    }
}