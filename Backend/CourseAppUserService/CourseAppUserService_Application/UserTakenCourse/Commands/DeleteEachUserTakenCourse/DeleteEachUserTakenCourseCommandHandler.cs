using CourseAppUserService_Application.Interfaces;
using MediatR;

namespace CourseAppUserService_Application.UserTakenCourse.Commands.DeleteEachUserTakenCourse;

public class DeleteEachUserTakenCourseCommandHandler(IUnitOfWork unitOfWork): IRequestHandler<DeleteEachUserTakenCourseCommand>
{
    public async Task Handle(DeleteEachUserTakenCourseCommand request, CancellationToken cancellationToken)
    {
        var records = await unitOfWork.UserTakenCourses.GetEachUserTakenCoursesByCourseIdAsync(request.CourseId, cancellationToken);
        
        foreach (var record in records)
            await unitOfWork.UserTakenCourses.RemoveEntityAsync(record);
        
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}