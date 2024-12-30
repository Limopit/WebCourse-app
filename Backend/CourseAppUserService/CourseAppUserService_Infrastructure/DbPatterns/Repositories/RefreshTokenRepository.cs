using CourseAppUserService_Application.Interfaces.Repositories;
using CourseAppUserService_Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CourseAppUserService_Persistance.DbPatterns.Repositories;

public class RefreshTokenRepository(UserServiceDbContext libraryDbContext) : IRefreshTokenRepository
{
    public async Task<RefreshToken?> ValidateRefreshTokenAsync(string refreshToken)
    {
        return await libraryDbContext.RefreshTokens
            .Where(token => token.Token == refreshToken && token.Expires > DateTime.UtcNow)
            .FirstOrDefaultAsync();
    }

    public async Task<RefreshToken?> RevokeTokenAsync(string refreshToken)
    {
        return await libraryDbContext.RefreshTokens
            .SingleOrDefaultAsync(token => token.Token == refreshToken);
    }
}