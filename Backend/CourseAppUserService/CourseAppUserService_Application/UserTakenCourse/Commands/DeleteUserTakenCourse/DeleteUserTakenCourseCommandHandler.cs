using CourseAppUserService_Application.Common.Exceptions;
using CourseAppUserService_Application.Interfaces;
using CourseAppUserService_Domain.Entities;
using MediatR;

namespace CourseAppUserService_Application.UserTakenCourse.Commands.DeleteUserTakenCourse;

public class DeleteUserTakenCourseCommandHandler(IUnitOfWork unitOfWork): IRequestHandler<DeleteUserTakenCourseCommand>
{
    public async Task Handle(DeleteUserTakenCourseCommand request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.Users.FindUserByEmailAsync(request.Email);
        if (user == null)
        {
            throw new NotFoundException(nameof(User), request.Email);
        }
        
        var record = await unitOfWork.UserTakenCourses.GetUserTakenCoursesByCourseIdAsync(request.CourseId, user, cancellationToken);
        if (record == null)
        {
            throw new NotFoundException(nameof(UserTakenCourse), request.CourseId);
        }
        
        await unitOfWork.UserTakenCourses.RemoveEntityAsync(record);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}