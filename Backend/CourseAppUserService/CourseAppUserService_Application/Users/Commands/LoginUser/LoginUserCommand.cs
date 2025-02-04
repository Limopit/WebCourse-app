using MediatR;

namespace CourseAppUserService_Application.Users.Commands.LoginUser;

public record LoginUserCommand: IRequest<(string, string)>
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}