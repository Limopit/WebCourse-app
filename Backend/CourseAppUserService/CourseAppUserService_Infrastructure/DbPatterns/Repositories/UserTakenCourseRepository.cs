using CourseAppUserService_Application.Interfaces.Repositories;
using CourseAppUserService_Domain;
using Microsoft.EntityFrameworkCore;

namespace CourseAppUserService_Persistance.DbPatterns.Repositories;

public class UserTakenCourseRepository(UserServiceDbContext context)
    : BaseRepository<UserTakenCourses>(context), IUserTakenCourseRepository
{
    public async Task<List<UserTakenCourses>> GetUserTakenCoursesAsync(string userId, CancellationToken token)
    {
        return await context.UserTakenCourses
            .Where(b => b.UserId == userId)
            .ToListAsync(token);
    }
}