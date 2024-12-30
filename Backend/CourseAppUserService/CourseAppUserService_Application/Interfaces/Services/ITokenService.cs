using CourseAppUserService_Domain.Entities;

namespace CourseAppUserService_Application.Interfaces.Services;

public interface ITokenService
{
    Task<(string accessToken, string refreshToken)> GenerateTokensAsync(User user, CancellationToken token);
    Task<string> GenerateNewTokenAsync(User user);
}