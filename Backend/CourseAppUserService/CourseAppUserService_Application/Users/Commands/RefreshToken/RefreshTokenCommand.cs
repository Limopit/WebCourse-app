using MediatR;

namespace CourseAppUserService_Application.Users.Commands.RefreshToken;

public record RefreshTokenCommand: IRequest<string>
{
    public required string RefreshToken { get; set; }
}