using CourseAppUserService_Domain;
using Microsoft.AspNetCore.Identity;

namespace CourseAppUserService_Application.Interfaces.Repositories;

public interface IUserRepository: IBaseRepository<User>
{
    Task<IdentityResult?> AddUserAsync(User user, string password);
    Task<User?> FindUserByEmail(string email);
    Task<User?> FindUserById(string id);
    Task<bool> UserRoleExistsAsync(string role);
    Task<IdentityResult?> GiveRoleAsync(User user, string role);
    Task<IdentityResult?> ClearUserRolesAsync(User user);
    Task<IdentityResult?> UpdateUserPassword(User user, string currentPassword, string newPassword);
    Task<bool> CheckPassword(User user, string password);
}