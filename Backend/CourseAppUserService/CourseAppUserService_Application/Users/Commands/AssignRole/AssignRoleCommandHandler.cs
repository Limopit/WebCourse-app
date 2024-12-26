using CourseAppUserService_Application.Common.Exceptions;
using CourseAppUserService_Application.Interfaces;
using CourseAppUserService_Domain;
using MediatR;

namespace CourseAppUserService_Application.Users.Commands.AssignRole;

public class AssignRoleCommandHandler : IRequestHandler<AssignRoleCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public AssignRoleCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(AssignRoleCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Users.FindUserByEmail(request.Email);
        if (user == null)
        {
            throw new NotFoundException(nameof(User), request.Email);
        }

        var removeResult = await _unitOfWork.Users.ClearUserRolesAsync(user);
        if (!removeResult.Succeeded)
        {
            throw new RoleAssignmentException("", "is failed to remove");
        }

        var addResult = await _unitOfWork.Users.GiveRoleAsync(user, request.Role);

        return addResult.Succeeded;
    }
}