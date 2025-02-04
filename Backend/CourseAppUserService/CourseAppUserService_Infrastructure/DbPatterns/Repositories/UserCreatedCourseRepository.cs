using CourseAppUserService_Application.Interfaces.Repositories;
using CourseAppUserService_Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CourseAppUserService_Persistance.DbPatterns.Repositories;

public class UserCreatedCourseRepository(UserServiceDbContext context)
    : BaseRepository<UserCreatedCourses>(context), IUserCreatedCourseRepository
{
    public async Task<List<UserCreatedCourses>> GetUserCreatedCoursesAsync(string userId, CancellationToken token)
    {
        return await context.UserCreatedCourses
            .Where(course => course.UserId == userId)
            .ToListAsync(token);
    }
    
    public async Task<UserCreatedCourses?> GetUserCreatedCourseByCourseIdAsync(string courseId, CancellationToken token)
    {
        return await context.UserCreatedCourses
            .Where(course => course.CourseId == courseId)
            .FirstOrDefaultAsync(token);
    }
}