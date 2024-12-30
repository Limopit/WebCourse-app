using CourseAppUserService_Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace CourseAppUserService_Application.Interfaces.Repositories;

public interface IUserRepository: IBaseRepository<User>
{
    Task<IdentityResult?> AddUserAsync(User user, string password);
    Task<User?> FindUserByEmailAsync(string email);
    Task<User?> FindUserByIdAsync(string id);
    Task<bool> UserRoleExistsAsync(string role);
    Task<IdentityResult?> GiveRoleAsync(User user, string role);
    Task<IdentityResult?> ClearUserRolesAsync(User user);
    Task<IdentityResult?> UpdateUserPasswordAsync(User user, string currentPassword, string newPassword);
    Task<bool> CheckPasswordAsync(User user, string password);
}