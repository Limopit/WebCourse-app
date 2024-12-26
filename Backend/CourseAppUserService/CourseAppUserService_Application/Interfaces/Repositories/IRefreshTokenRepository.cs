using CourseAppUserService_Domain;

namespace CourseAppUserService_Application.Interfaces.Repositories;

public interface IRefreshTokenRepository
{
    Task<RefreshToken?> ValidateRefreshToken(string token);
    Task<RefreshToken?> RevokeToken(string refreshToken);
}