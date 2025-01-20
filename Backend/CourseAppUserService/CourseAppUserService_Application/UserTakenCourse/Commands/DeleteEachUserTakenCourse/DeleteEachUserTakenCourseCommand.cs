using MediatR;

namespace CourseAppUserService_Application.UserTakenCourse.Commands.DeleteEachUserTakenCourse;

public record DeleteEachUserTakenCourseCommand: IRequest
{
    public required string CourseId { get; set; }
}