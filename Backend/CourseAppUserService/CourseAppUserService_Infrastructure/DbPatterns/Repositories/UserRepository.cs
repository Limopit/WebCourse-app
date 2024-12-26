using CourseAppUserService_Application.Interfaces.Repositories;
using CourseAppUserService_Domain;
using Microsoft.AspNetCore.Identity;

namespace CourseAppUserService_Persistance.DbPatterns.Repositories;

public class UserRepository: IUserRepository
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UserRepository(SignInManager<User> signInManager, 
        UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _roleManager = roleManager;
    }
    
    public async Task<IdentityResult?> AddUserAsync(User user, string password)
    {
        return await _userManager.CreateAsync(user, password);
    }

    public async Task<SignInResult?> SignInAsync(string email, string password, bool isPersistent, bool lockoutOnFailure)
    {
        return await _signInManager.PasswordSignInAsync(email, password, isPersistent, lockoutOnFailure);
    }

    public async Task<User?> FindUserByEmail(string email)
    {
        return await _userManager.FindByEmailAsync(email);
    }

    public async Task<User?> FindUserById(string id)
    {
        return await _userManager.FindByIdAsync(id);
    }

    public async Task<bool> UserRoleExistsAsync(string role)
    {
        return await _roleManager.RoleExistsAsync(role);
    }

    public async Task<IdentityResult?> GiveRoleAsync(User user, string role)
    {
        return await _userManager.AddToRoleAsync(user, role);
    }

    public async Task<IdentityResult?> ClearUserRolesAsync(User user)
    {
        var roles = await _userManager.GetRolesAsync(user);
        return await _userManager.RemoveFromRolesAsync(user, roles);
    }
}