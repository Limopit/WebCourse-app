using MediatR;

namespace CourseAppUserService_Application.Users.Commands.AssignRole;

public class AssignRoleCommand: IRequest<bool>
{
    public string Email { get; set; }
    public string Role { get; set; }
}