using CourseAppUserService_Application.Interfaces.Repositories;

namespace CourseAppUserService_Application.Interfaces;

public interface IUnitOfWork
{
    public IUserRepository Users { get; set; }
    public IUserCreatedCourseRepository UserCreatedCourses { get; set; }
    public IUserTakenCourseRepository UserTakenCourses { get; set; }
    public IRefreshTokenRepository RefreshTokens { get; set; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}