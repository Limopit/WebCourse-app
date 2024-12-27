using CourseAppUserService_Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CourseAppUserService_Persistance;

public class DbInitializer
{
    static Guid AdminId = Guid.NewGuid();
    static Guid UserId = Guid.NewGuid();
    public static async Task Initialize(UserServiceDbContext context, UserManager<User> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        context.Users.RemoveRange(context.Users);
        context.UserCreatedCourses.RemoveRange(context.UserCreatedCourses);
        context.UserTakenCourses.RemoveRange(context.UserTakenCourses);
        context.RefreshTokens.RemoveRange(context.RefreshTokens);
        await context.SaveChangesAsync();

        await context.Database.MigrateAsync();
        
        await SeedUsersAndRolesAsync(userManager, roleManager);
        SeedDatabase(context);
    }
    
    private static void SeedDatabase(UserServiceDbContext context)
    {
        var takenCourseRecordIdA = Guid.NewGuid();
        var takenCourseRecordIdB = Guid.NewGuid();
        var createdCourseRecordIdA = Guid.NewGuid();
        var createdCourseRecordIdB = Guid.NewGuid();

        var takenCourses = new[]
        {
            new UserTakenCourses()
            {
                RecordId = takenCourseRecordIdA,
                CourseId = "firstCourse",
                UserId = AdminId.ToString(),
                DateStart = DateTime.Now,
                Status = CompletionStatus.InProgress.ToString()
            },
            new UserTakenCourses()
            {
                RecordId = takenCourseRecordIdB,
                CourseId = "secondCourse",
                UserId = UserId.ToString(),
                DateStart = DateTime.Now,
                Status = CompletionStatus.InProgress.ToString()
            }
        };
        
        var createdCourses = new[]
        {
            new UserCreatedCourses()
            {
                RecordId = createdCourseRecordIdA,
                CourseId = "thirdCourse",
                UserId = AdminId.ToString(),
                ApprovementDate = null,
                ApprovementStatus = ApprovementStatus.Pending.ToString()
            },
            new UserCreatedCourses()
            {
                RecordId = createdCourseRecordIdB,
                CourseId = "fourthCourse",
                UserId = AdminId.ToString(),
                ApprovementDate = DateTime.Now,
                ApprovementStatus = ApprovementStatus.Accepted.ToString()
            }
        };
        context.UserTakenCourses.AddRange(takenCourses);
        context.UserCreatedCourses.AddRange(createdCourses);
        
        context.SaveChanges();
    }
    
    private static async Task SeedUsersAndRolesAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {
        string[] roles = { "Admin", "User" };
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        if (await userManager.FindByEmailAsync("admin@email.com") == null)
        {
            var admin = new User
            {
                Id = AdminId.ToString(),
                FirstName = "Admin",
                LastName = "Admin",
                UserName = "admin",
                Email = "admin@gmail.com",
                Birthday = new DateTime(1999, 2, 20),
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            var result = await userManager.CreateAsync(admin, "Admin123#");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(admin, "Admin");
            }
        }

        if (await userManager.FindByEmailAsync("user@email.com") == null)
        {
            var user = new User
            {
                Id = UserId.ToString(),
                FirstName = "User",
                LastName = "User",
                UserName = "user",
                Email = "user@gmail.com",
                Birthday = new DateTime(1999, 2, 20),
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            var result = await userManager.CreateAsync(user, "User123#");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "User");
            }
        }
        
    }
}