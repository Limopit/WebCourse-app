using MediatR;

namespace CourseAppUserService_Application.UserTakenCourse.Commands.DeleteUserTakenCourse;

public record DeleteUserTakenCourseCommand: IRequest
{
    public required string CourseId { get; set; }
}