using CourseAppUserService_Application.Common.Exceptions;
using CourseAppUserService_Application.Interfaces;
using CourseAppUserService_Application.Interfaces.Services;
using CourseAppUserService_Domain.Entities;
using MediatR;

namespace CourseAppUserService_Application.UserCreatedCourse.Commands.CreateUserCreatedCourse;

public class CreateUserCreatedCourseCommandHandler(IUnitOfWork unitOfWork, IMapperService mapper):
    IRequestHandler<CreateUserCreatedCourseCommand, Guid> 
//After implementing Course Service courseId connecting must be updated
{
    public async Task<Guid> Handle(CreateUserCreatedCourseCommand request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.Users.FindUserByEmailAsync(request.Email);
        
        if (user == null)
        {
            throw new NotFoundException(nameof(User), request.Email);
        }

        var userCreatedCourse = await mapper.MapAsync<CreateUserCreatedCourseCommand, UserCreatedCourses>(request);
        
        userCreatedCourse.UserId = user.Id;
        
        await unitOfWork.UserCreatedCourses.AddEntityAsync(userCreatedCourse, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        
        return userCreatedCourse.RecordId;
    }
}