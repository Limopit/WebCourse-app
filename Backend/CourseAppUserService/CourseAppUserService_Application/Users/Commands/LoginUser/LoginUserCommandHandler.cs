using CourseAppUserService_Application.Interfaces;
using CourseAppUserService_Application.Interfaces.Services;
using MediatR;

namespace CourseAppUserService_Application.Users.Commands.LoginUser;

public class LoginUserCommandHandler(IUnitOfWork unitOfWork, ITokenService tokenService)
    : IRequestHandler<LoginUserCommand, (string, string)>
{
    public async Task<(string, string)> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.Users.FindUserByEmail(request.Email);
        if (user == null)
        {
            throw new Exception("Wrong email or password");
        }
        
        var isPasswordValid = await unitOfWork.Users.CheckPassword(user, request.Password);
        if (!isPasswordValid)
        {
            throw new Exception("Wrong email or password");
        }

        return await tokenService.GenerateTokens(user, cancellationToken);
    }
}