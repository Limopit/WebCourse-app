using CourseAppUserService_Application.Common.Exceptions;
using CourseAppUserService_Application.Interfaces;
using CourseAppUserService_Domain;
using MediatR;

namespace CourseAppUserService_Application.Users.Commands.RegisterUser;

public class RegisterUserCommandHandler: IRequestHandler<RegisterUserCommand, string>
{
    private readonly IUnitOfWork _unitOfWork;

    public RegisterUserCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<string> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User { UserName = request.Email, Email = request.Email,
            FirstName = request.FirstName, LastName = request.LastName};
        
        if (!await _unitOfWork.Users.UserRoleExistsAsync(request.Role))
        {
            throw new RoleAssignmentException(request.Role, " does not exist");
        }
        
        var result = await _unitOfWork.Users.AddUserAsync(user, request.Password);
        
        if (!result.Succeeded)
        {
            throw new Exception(string.Join(", ", 
                result.Errors.Select(e => e.Description)));
        }
        
        await _unitOfWork.Users.GiveRoleAsync(user, request.Role);

        return user.Id;
    }
}