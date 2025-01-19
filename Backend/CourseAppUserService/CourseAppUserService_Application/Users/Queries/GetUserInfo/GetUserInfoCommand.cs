using MediatR;

namespace CourseAppUserService_Application.Users.Queries.GetUserInfo;

public record GetUserInfoCommand: IRequest<UserDto>
{
    public required string Email { get; set; }
}