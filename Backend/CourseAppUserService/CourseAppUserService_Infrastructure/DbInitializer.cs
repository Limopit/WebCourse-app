using CourseAppUserService_Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CourseAppUserService_Persistance;

public class DbInitializer
{
    public static async Task Initialize(UserServiceDbContext context, UserManager<User> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        context.Users.RemoveRange(context.Users);
        context.UserCreatedCourses.RemoveRange(context.UserCreatedCourses);
        context.UserTakenCourses.RemoveRange(context.UserTakenCourses);
        context.RefreshTokens.RemoveRange(context.RefreshTokens);

        await context.Database.MigrateAsync();
    }
}