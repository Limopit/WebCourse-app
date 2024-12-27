using CourseAppUserService_Application.Common.Exceptions;
using CourseAppUserService_Application.Interfaces;
using CourseAppUserService_Application.Interfaces.Services;
using MediatR;

namespace CourseAppUserService_Application.Users.Commands.LoginUser;

public class LoginUserCommandHandler(IUnitOfWork unitOfWork, ITokenService tokenService)
    : IRequestHandler<LoginUserCommand, (string, string)>
{
    public async Task<(string, string)> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.Users.FindUserByEmailAsync(request.Email);
        if (user == null)
        {
            throw new InvalidEmailException();
        }
        
        var isPasswordValid = await unitOfWork.Users.CheckPasswordAsync(user, request.Password);
        if (!isPasswordValid)
        {
            throw new InvalidPasswordException();
        }

        return await tokenService.GenerateTokensAsync(user, cancellationToken);
    }
}