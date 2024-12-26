using CourseAppUserService_Domain;
using Microsoft.AspNetCore.Identity;

namespace CourseAppUserService_Application.Interfaces.Services;

public interface ITokenService
{
    Task<(string accessToken, string refreshToken)> GenerateTokens(User user,
        UserManager<User> userManager, CancellationToken token);

    Task<string> GenerateNewToken(User user, UserManager<User> userManager);
}