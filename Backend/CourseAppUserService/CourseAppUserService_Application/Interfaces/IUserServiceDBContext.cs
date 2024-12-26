using CourseAppUserService_Domain;
using Microsoft.EntityFrameworkCore;

namespace CourseAppUserService_Application.Interfaces;

public interface IUserServiceDbContext
{
    DbSet<User> Users { get; set; }
    DbSet<UserCreatedCourses> UserCreatedCourses { get; set; }
    DbSet<UserTakenCourses> UserTakenCourses { get; set; }
    DbSet<RefreshToken> RefreshTokens { get; set; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}