using CourseAppUserService_Application.Common.Exceptions;
using CourseAppUserService_Application.Interfaces;
using CourseAppUserService_Application.Interfaces.Services;
using MediatR;

namespace CourseAppUserService_Application.Users.Commands.RefreshToken;

public class RefreshTokenCommandHandler(
    IUnitOfWork unitOfWork,
    ITokenService tokenService)
    : IRequestHandler<RefreshTokenCommand, string>
{
    public async Task<string> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var validatedToken = await unitOfWork.RefreshTokens.ValidateRefreshTokenAsync(request.RefreshToken);
        
        if (validatedToken == null)
        {
            throw new UnauthorizedAccessException();
        }
        
        var userId = validatedToken.UserId;

        var user = await unitOfWork.Users.FindUserByIdAsync(userId);

        var newJWT = await tokenService.GenerateNewTokenAsync(user);
        
        var token = await unitOfWork.RefreshTokens.RevokeTokenAsync(request.RefreshToken);
        
        if (token == null)
        {
            throw new NotFoundException(nameof(RefreshToken), request.RefreshToken);
        }
        
        token.Revoked = DateTime.UtcNow;
        
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return newJWT;
    }
}