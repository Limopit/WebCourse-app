using MediatR;

namespace CourseAppUserService_Application.Users.Commands.LoginUser;

public class LoginUserCommand: IRequest<(string, string)>
{
    public string Email { get; set; }
    public string Password { get; set; }
}