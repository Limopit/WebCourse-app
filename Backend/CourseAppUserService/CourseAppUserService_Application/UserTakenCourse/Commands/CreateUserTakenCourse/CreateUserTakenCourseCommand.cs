using MediatR;

namespace CourseAppUserService_Application.UserTakenCourse.Commands.CreateUserTakenCourse;

public class CreateUserTakenCourseCommand: IRequest<Guid>
{
    public required string Email { get; set; }
    public required string CourseId { get; set; }
    public required string Status { get; set; } = "InProgress";
    public required DateTime StartDate { get; set; } = DateTime.UtcNow;
}