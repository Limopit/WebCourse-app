using MediatR;

namespace CourseAppUserService_Application.Users.Commands.DeleteUser;

public class DeleteUserCommand: IRequest
{
    public string Email { get; set; }
}