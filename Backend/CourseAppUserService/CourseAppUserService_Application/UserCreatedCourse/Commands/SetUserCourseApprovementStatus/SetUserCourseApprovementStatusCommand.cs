using CourseAppUserService_Domain.Enums;
using MediatR;

namespace CourseAppUserService_Application.UserCreatedCourse.Commands.SetUserCourseApprovementStatus;

public record SetUserCourseApprovementStatusCommand: IRequest
{
    public string CourseId { get; set; }
    public ApprovementStatus Status { get; set; }
}