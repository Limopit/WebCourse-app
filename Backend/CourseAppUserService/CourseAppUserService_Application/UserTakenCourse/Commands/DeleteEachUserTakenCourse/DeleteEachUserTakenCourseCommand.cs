using MediatR;

namespace CourseAppUserService_Application.UserTakenCourse.Commands.DeleteEachUserTakenCourse;

public record DeleteEachUserTakenCourseCommand: IRequest
{
    public required string Id { get; set; }
}