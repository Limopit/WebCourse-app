using CourseAppUserService_Application.Interfaces;
using CourseAppUserService_Application.Interfaces.Services;
using MediatR;

namespace CourseAppUserService_Application.Users.Commands.UpdateUserPassword;

public class UpdateUserPasswordCommandHandler(IUnitOfWork unitOfWork, IHttpContextService httpContextService)
    : IRequestHandler<UpdateUserPasswordCommand>
{
    public async Task Handle(UpdateUserPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await httpContextService.GetCurrentUserEmailAsync();
        if (user == null)
        {
            throw new UnauthorizedAccessException("You are not logged in");
        }

        var currentUser = await unitOfWork.Users.FindUserByEmailAsync(user);
        
        var isValid = await unitOfWork.Users.CheckPasswordAsync(currentUser, request.OldPassword);
        if (!isValid)
        {
            throw new UnauthorizedAccessException("Invalid password");
        }
        
        await unitOfWork.Users.UpdateUserPasswordAsync(currentUser, request.OldPassword, request.NewPassword);
    }
}