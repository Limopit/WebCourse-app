using CourseAppUserService_Domain;

namespace CourseAppUserService_Application.Interfaces.Services;

public interface ITokenService
{
    Task<(string accessToken, string refreshToken)> GenerateTokens(User user, CancellationToken token);
    Task<string> GenerateNewToken(User user);
}