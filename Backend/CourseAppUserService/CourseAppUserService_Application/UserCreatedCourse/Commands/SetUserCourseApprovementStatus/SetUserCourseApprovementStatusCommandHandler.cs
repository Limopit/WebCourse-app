using CourseAppUserService_Application.Common.Exceptions;
using CourseAppUserService_Application.Interfaces;
using CourseAppUserService_Domain.Enums;
using MediatR;

namespace CourseAppUserService_Application.UserCreatedCourse.Commands.SetUserCourseApprovementStatus;

public class SetUserCourseApprovementStatusCommandHandler(IUnitOfWork unitOfWork): IRequestHandler<SetUserCourseApprovementStatusCommand>
{
    public async Task Handle(SetUserCourseApprovementStatusCommand request, CancellationToken cancellationToken)
    {
        var course = await unitOfWork.UserCreatedCourses.GetUserCreatedCourseByCourseIdAsync(request.CourseId, cancellationToken);
        if (course == null)
        {
            throw new NotFoundException(nameof(UserCreatedCourse), request.CourseId);
        }
        
        course.ApprovementStatus = request.Status.ToString();
        if (request.Status == ApprovementStatus.Accepted)
        {
            course.ApprovementDate = DateTime.UtcNow;
        }
        
        await unitOfWork.UserCreatedCourses.UpdateAsync(course);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}