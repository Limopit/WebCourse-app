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

    public UnitOfWork(UserServiceDbContext dbContext, UserManager<User> userManager,
        SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager)
    {
        _dbContext = dbContext;
        Users = new UserRepository(signInManager, userManager, roleManager);
    }
    
    public async Task<int> SaveChangesAsync(CancellationToken token)
    {
        return await _dbContext.SaveChangesAsync(token);
    }
}