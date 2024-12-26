using CourseAppUserService_Application.Common.Exceptions;
using CourseAppUserService_Application.Interfaces;
using CourseAppUserService_Application.Interfaces.Services;
using CourseAppUserService_Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace CourseAppUserService_Application.Users.Commands.RefreshToken;

public class RefreshTokenCommandHandler(
    IUnitOfWork unitOfWork,
    ITokenService tokenService,
    UserManager<User> userManager)
    : IRequestHandler<RefreshTokenCommand, string>
{
    public async Task<string> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var validatedToken = await unitOfWork.RefreshTokens.ValidateRefreshToken(request.RefreshToken);
        
        if (validatedToken == null)
        {
            throw new UnauthorizedAccessException();
        }
        
        var userId = validatedToken.UserId;

        var user = await unitOfWork.Users.FindUserById(userId);

        var newJWT = await tokenService.GenerateNewToken(user, userManager);
        
        var token = await unitOfWork.RefreshTokens.RevokeToken(request.RefreshToken);
        
        if (token == null)
        {
            throw new NotFoundException(nameof(RefreshToken), request.RefreshToken);
        }
        
        token.Revoked = DateTime.UtcNow;
        
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return newJWT;
    }
}