using MediatR;

namespace CourseAppUserService_Application.UserTakenCourse.Commands.DeleteUserTakenCourse;

public class DeleteUserTakenCourseCommand: IRequest
{
    public string CourseId { get; set; }
}