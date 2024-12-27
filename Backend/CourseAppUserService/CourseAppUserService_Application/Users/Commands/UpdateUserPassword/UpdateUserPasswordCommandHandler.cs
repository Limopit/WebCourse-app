using CourseAppUserService_Application.Interfaces;
using CourseAppUserService_Application.Interfaces.Services;
using MediatR;

namespace CourseAppUserService_Application.Users.Commands.UpdateUserPassword;

public class UpdateUserPasswordCommandHandler(IUnitOfWork unitOfWork, IHttpContextService httpContextService)
    : IRequestHandler<UpdateUserPasswordCommand>
{
    public async Task Handle(UpdateUserPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await httpContextService.GetCurrentUserEmail();
        if (user == null)
        {
            throw new UnauthorizedAccessException("You are not logged in");
        }

        var currentUser = await unitOfWork.Users.FindUserByEmail(user);
        
        var isValid = await unitOfWork.Users.CheckPassword(currentUser, request.PrevPassword);
        if (!isValid)
        {
            throw new UnauthorizedAccessException("Invalid password");
        }
        
        await unitOfWork.Users.UpdateUserPassword(currentUser, request.PrevPassword, request.NewPassword);
    }
}