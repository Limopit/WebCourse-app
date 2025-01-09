using MediatR;

namespace CourseAppUserService_Application.Users.Commands.LoginUser;

public class LoginUserCommand: IRequest<(string, string)>
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}