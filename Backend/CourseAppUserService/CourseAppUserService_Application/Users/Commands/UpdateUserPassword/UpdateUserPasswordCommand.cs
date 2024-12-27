using MediatR;

namespace CourseAppUserService_Application.Users.Commands.UpdateUserPassword;

public class UpdateUserPasswordCommand: IRequest
{
    public string PrevPassword { get; set; }
    public string NewPassword { get; set; }
}