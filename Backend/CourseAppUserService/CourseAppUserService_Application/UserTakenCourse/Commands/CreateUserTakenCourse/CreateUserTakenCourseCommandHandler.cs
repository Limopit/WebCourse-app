using CourseAppUserService_Application.Common.Exceptions;
using CourseAppUserService_Application.Interfaces;
using CourseAppUserService_Application.Interfaces.Services;
using CourseAppUserService_Domain.Entities;
using CourseAppUserService_Domain.Enums;
using MediatR;

namespace CourseAppUserService_Application.UserTakenCourse.Commands.CreateUserTakenCourse;

public class CreateUserTakenCourseCommandHandler(IUnitOfWork unitOfWork, IMapperService mapper)
    : IRequestHandler<CreateUserTakenCourseCommand, Guid>
{
    public async Task<Guid> Handle(CreateUserTakenCourseCommand request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.Users.FindUserByEmailAsync(request.Email);
        
        if (user == null)
        {
            throw new NotFoundException(nameof(User), request.Email);
        }
        
        var userTakenCourse = await mapper.MapAsync<CreateUserTakenCourseCommand, UserTakenCourses>(request);
        
        userTakenCourse.UserId = user.Id;
        userTakenCourse.Status = CompletionStatus.InProgress.ToString();
        
        await unitOfWork.UserTakenCourses.AddEntityAsync(userTakenCourse, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        
        return userTakenCourse.RecordId;
    }
}