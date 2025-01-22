using MediatR;

namespace CourseAppUserService_Application.UserTakenCourse.Commands.DeleteUserTakenCourse;

public class DeleteUserTakenCourseCommand: IRequest
{
    public required string Id { get; set; }
    public required string Email { get; set; }
}