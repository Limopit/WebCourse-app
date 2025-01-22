using MediatR;

namespace CourseAppUserService_Application.Users.Commands.DeleteUser;

public record DeleteUserCommand: IRequest
{
    public required string Email { get; set; }
}