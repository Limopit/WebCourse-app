using MediatR;

namespace CourseAppUserService_Application.Users.Commands.UpdateUserPassword;

public class UpdateUserPasswordCommand: IRequest
{
    public required string OldPassword { get; set; }
    public required string NewPassword { get; set; }
}