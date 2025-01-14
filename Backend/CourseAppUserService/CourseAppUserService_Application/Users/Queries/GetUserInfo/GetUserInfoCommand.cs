using MediatR;

namespace CourseAppUserService_Application.Users.Queries.GetUserInfo;

public class GetUserInfoCommand: IRequest<UserDto>
{
    public required string Email { get; set; }
}