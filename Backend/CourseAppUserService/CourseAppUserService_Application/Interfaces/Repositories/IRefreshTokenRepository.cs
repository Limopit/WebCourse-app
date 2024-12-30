using CourseAppUserService_Domain.Entities;

namespace CourseAppUserService_Application.Interfaces.Repositories;

public interface IRefreshTokenRepository
{
    Task<RefreshToken?> ValidateRefreshTokenAsync(string token);
    Task<RefreshToken?> RevokeTokenAsync(string refreshToken);
}