using MediatR;

namespace CourseAppUserService_Application.Users.Commands.UpdateUserData;

public class UpdateUserDataCommand: IRequest
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
}