using MediatR;

namespace CourseAppUserService_Application.Users.Queries.GetUser;

public record GetUserCommand: IRequest<UserDto>
{
    public required string Email { get; set; }
}