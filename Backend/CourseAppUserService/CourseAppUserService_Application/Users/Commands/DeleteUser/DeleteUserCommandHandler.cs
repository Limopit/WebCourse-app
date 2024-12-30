using CourseAppUserService_Application.Common.Exceptions;
using CourseAppUserService_Application.Interfaces;
using CourseAppUserService_Domain.Entities;
using MediatR;

namespace CourseAppUserService_Application.Users.Commands.DeleteUser;

public class DeleteUserCommandHandler(IUnitOfWork unitOfWork):IRequestHandler<DeleteUserCommand>
{
    public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.Users.FindUserByEmailAsync(request.Email);
        if (user == null)
        {
            throw new NotFoundException(nameof(User), request.Email);
        }

        await unitOfWork.Users.RemoveEntityAsync(user);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}