using MediatR;

namespace CourseAppUserService_Application.Users.Commands.UpdateUserData;

public record UpdateUserDataCommand: IRequest
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string Username { get; set; }
}