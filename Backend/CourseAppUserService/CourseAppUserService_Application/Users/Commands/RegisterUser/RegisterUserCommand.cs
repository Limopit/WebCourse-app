using MediatR;

namespace CourseAppUserService_Application.Users.Commands.RegisterUser;

public record RegisterUserCommand: IRequest<string>
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string Role { get; set; }
}