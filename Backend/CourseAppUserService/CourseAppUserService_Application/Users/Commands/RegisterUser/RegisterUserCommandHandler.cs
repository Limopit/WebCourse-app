using CourseAppUserService_Application.Common.Exceptions;
using CourseAppUserService_Application.Interfaces;
using CourseAppUserService_Domain.Entities;
using MediatR;

namespace CourseAppUserService_Application.Users.Commands.RegisterUser;

public class RegisterUserCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<RegisterUserCommand, string>
{
    public async Task<string> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User { 
            UserName = request.Email,
            Email = request.Email,
            FirstName = request.FirstName, 
            LastName = request.LastName
        };

        var roleExists = await unitOfWork.Users.UserRoleExistsAsync(request.Role);
        
        if (!roleExists)
        {
            throw new RoleAssignmentException(request.Role, " does not exist");
        }
        
        var result = await unitOfWork.Users.AddUserAsync(user, request.Password);
        
        if (!result.Succeeded)
        {
            throw new Exception(string.Join(", ", 
                result.Errors.Select(error => error.Description)));
        }
        
        await unitOfWork.Users.GiveRoleAsync(user, request.Role);

        return user.Id;
    }
}