using MediatR;

namespace CourseAppUserService_Application.Users.Queries.GetUserInfo;

public class GetUserInfoCommand: IRequest<UserDto>
{
    public string Email { get; set; }
}