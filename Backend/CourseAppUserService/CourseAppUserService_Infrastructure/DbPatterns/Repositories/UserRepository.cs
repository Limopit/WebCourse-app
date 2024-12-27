using CourseAppUserService_Application.Interfaces.Repositories;
using CourseAppUserService_Domain;
using Microsoft.AspNetCore.Identity;

namespace CourseAppUserService_Persistance.DbPatterns.Repositories;

public class UserRepository(
    UserServiceDbContext context,
    UserManager<User> userManager,
    RoleManager<IdentityRole> roleManager)
    : BaseRepository<User>(context), IUserRepository
{
    public async Task<IdentityResult?> AddUserAsync(User user, string password)
    {
        return await userManager.CreateAsync(user, password);
    }

    public async Task<User?> FindUserByEmail(string email)
    {
        return await userManager.FindByEmailAsync(email);
    }

    public async Task<User?> FindUserById(string id)
    {
        return await userManager.FindByIdAsync(id);
    }

    public async Task<bool> UserRoleExistsAsync(string role)
    {
        return await roleManager.RoleExistsAsync(role);
    }

    public async Task<IdentityResult?> GiveRoleAsync(User user, string role)
    {
        return await userManager.AddToRoleAsync(user, role);
    }

    public async Task<IdentityResult?> ClearUserRolesAsync(User user)
    {
        var roles = await userManager.GetRolesAsync(user);
        return await userManager.RemoveFromRolesAsync(user, roles);
    }

    public async Task<IdentityResult?> UpdateUserPassword(User user, string currentPassword, string newPassword)
    {
        return await userManager.ChangePasswordAsync(user, currentPassword, newPassword);
    }

    public async Task<bool> CheckPassword(User user, string password)
    {
        return await userManager.CheckPasswordAsync(user, password);
    }
    
}