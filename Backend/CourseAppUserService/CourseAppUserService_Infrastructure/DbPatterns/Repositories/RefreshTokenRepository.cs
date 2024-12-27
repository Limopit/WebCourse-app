using CourseAppUserService_Application.Interfaces.Repositories;
using CourseAppUserService_Domain;
using Microsoft.EntityFrameworkCore;

namespace CourseAppUserService_Persistance.DbPatterns.Repositories;

public class RefreshTokenRepository(UserServiceDbContext libraryDbContext) : IRefreshTokenRepository
{
    public async Task<RefreshToken?> ValidateRefreshToken(string refreshToken)
    {
        return await libraryDbContext.RefreshTokens
            .Where(token => token.Token == refreshToken && token.Expires > DateTime.UtcNow)
            .FirstOrDefaultAsync();
    }

    public async Task<RefreshToken?> RevokeToken(string refreshToken)
    {
        return await libraryDbContext.RefreshTokens
            .SingleOrDefaultAsync(token => token.Token == refreshToken);
    }
}