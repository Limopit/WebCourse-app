using CourseAppUserService_Application.Interfaces;
using CourseAppUserService_Application.Interfaces.Repositories;
using CourseAppUserService_Domain;
using CourseAppUserService_Persistance.DbPatterns.Repositories;
using Microsoft.AspNetCore.Identity;

namespace CourseAppUserService_Persistance.DbPatterns;

public class UnitOfWork: IUnitOfWork
{
    public IUserRepository Users { get; set; }
    public IUserCreatedCourseRepository UserCreatedCourses { get; set; }
    public IUserTakenCourseRepository UserTakenCourses { get; set; }
    public IRefreshTokenRepository RefreshTokens { get; set; }
    
    private readonly UserServiceDbContext _dbContext;

    public UnitOfWork(
        UserServiceDbContext dbContext,
        UserManager<User> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        _dbContext = dbContext;
        Users = new UserRepository(_dbContext, userManager, roleManager);
        UserTakenCourses = new UserTakenCourseRepository(_dbContext);
        UserCreatedCourses = new UserCreatedCourseRepository(_dbContext);
        RefreshTokens = new RefreshTokenRepository(_dbContext);
    }
    
    public async Task<int> SaveChangesAsync(CancellationToken token)
    {
        return await _dbContext.SaveChangesAsync(token);
    }
}