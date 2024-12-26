using CourseAppUserService_Application.Common.Exceptions;
using CourseAppUserService_Application.Interfaces;
using CourseAppUserService_Application.Interfaces.Services;
using CourseAppUserService_Domain;
using MediatR;

namespace CourseAppUserService_Application.UserTakenCourse.Commands.CreateUserTakenCourse;

public class CreateUserTakenCourseCommandHandler(IUnitOfWork unitOfWork, IMapperService mapper)
    : IRequestHandler<CreateUserTakenCourseCommand, Guid>
//After implementing Course Service courseId connecting must be updated
{
    public async Task<Guid> Handle(CreateUserTakenCourseCommand request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.Users.FindUserByEmail(request.Email);
        
        if (user == null)
        {
            throw new NotFoundException(nameof(User), request.Email);
        }
        
        var userTakenCourse = await mapper.Map<CreateUserTakenCourseCommand, UserTakenCourses>(request);
        
        userTakenCourse.UserId = user.Id;
        
        await unitOfWork.UserTakenCourses.AddEntityAsync(userTakenCourse, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        
        return userTakenCourse.RecordId;
    }
}