using MediatR;

namespace CourseAppUserService_Application.Users.Commands.AssignRole;

public record AssignRoleCommand: IRequest<bool>
{
    public required string Email { get; set; }
    public required string Role { get; set; }
}