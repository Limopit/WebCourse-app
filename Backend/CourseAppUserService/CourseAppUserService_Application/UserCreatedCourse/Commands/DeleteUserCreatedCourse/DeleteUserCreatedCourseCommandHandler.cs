using CourseAppUserService_Application.Common.Exceptions;
using CourseAppUserService_Application.Interfaces;
using MediatR;

namespace CourseAppUserService_Application.UserCreatedCourse.Commands.DeleteUserCreatedCourse;

public class DeleteUserCreatedCourseCommandHandler(IUnitOfWork unitOfWork): IRequestHandler<DeleteUserCreatedCourseCommand>
{
    public async Task Handle(DeleteUserCreatedCourseCommand request, CancellationToken cancellationToken)
    {
        var record = await unitOfWork.UserCreatedCourses.GetUserCreatedCourseByCourseIdAsync(request.Id, cancellationToken);
        if (record is null)
        {
            throw new NotFoundException(nameof(UserCreatedCourse), request.Id);
        }
        
        await unitOfWork.UserCreatedCourses.RemoveEntityAsync(record);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}