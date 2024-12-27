using CourseAppUserService_Application.Interfaces;
using CourseAppUserService_Application.Interfaces.Services;
using MediatR;

namespace CourseAppUserService_Application.Users.Commands.UpdateUserData;

public class UpdateUserDataCommandHandler(IHttpContextService httpContextService, IMapperService mapperService, IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateUserDataCommand>
{
    public async Task Handle(UpdateUserDataCommand request, CancellationToken cancellationToken)
    {
        var currentUser = await httpContextService.GetCurrentUserEmailAsync();

        if (currentUser == null)
        {
            throw new UnauthorizedAccessException("You are not logged in");
        }

        var user = await unitOfWork.Users.FindUserByEmailAsync(currentUser);
        
        await mapperService.UpdateAsync(request, user);
        await unitOfWork.Users.UpdateAsync(user);
        
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}