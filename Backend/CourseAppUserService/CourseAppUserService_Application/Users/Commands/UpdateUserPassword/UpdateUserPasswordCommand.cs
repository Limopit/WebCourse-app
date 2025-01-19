using MediatR;

namespace CourseAppUserService_Application.Users.Commands.UpdateUserPassword;

public record UpdateUserPasswordCommand: IRequest
{
    public required string OldPassword { get; set; }
    public required string NewPassword { get; set; }
}