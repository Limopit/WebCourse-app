using CourseAppUserService_Domain;
using MediatR;

namespace CourseAppUserService_Application.UserCreatedCourse.Commands.CreateUserCreatedCourse;

public class CreateUserCreatedCourseCommand: IRequest<Guid>
{
    public required string Email { get; set; }
    public required string CourseId { get; set; }
    public required ApprovementStatus ApprovementStatus { get; set; }
}