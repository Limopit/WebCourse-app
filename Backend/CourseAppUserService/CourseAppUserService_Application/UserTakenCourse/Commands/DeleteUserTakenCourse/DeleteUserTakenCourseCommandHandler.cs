using CourseAppUserService_Application.Common.Exceptions;
using CourseAppUserService_Application.Interfaces;
using MediatR;

namespace CourseAppUserService_Application.UserTakenCourse.Commands.DeleteUserTakenCourse;

public class DeleteUserTakenCourseCommandHandler(IUnitOfWork unitOfWork): IRequestHandler<DeleteUserTakenCourseCommand>
{
    public async Task Handle(DeleteUserTakenCourseCommand request, CancellationToken cancellationToken)
    {
        var records = await unitOfWork.UserTakenCourses.GetUserTakenCoursesByCourseIdAsync(request.CourseId, cancellationToken);
        
        foreach (var record in records)
            await unitOfWork.UserTakenCourses.RemoveEntityAsync(record);
        
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}