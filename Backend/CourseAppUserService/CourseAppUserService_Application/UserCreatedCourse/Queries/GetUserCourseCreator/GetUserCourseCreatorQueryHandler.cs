using CourseAppUserService_Application.Common.Exceptions;
using CourseAppUserService_Application.Interfaces;
using MediatR;

namespace CourseAppUserService_Application.UserCreatedCourse.Queries.GetUserCourseCreator;

public class GetUserCourseCreatorQueryHandler(IUnitOfWork unitOfWork): IRequestHandler<GetUserCourseCreatorQuery, string>
{
    public async Task<string> Handle(GetUserCourseCreatorQuery request, CancellationToken cancellationToken)
    {
        var course = await unitOfWork.UserCreatedCourses.GetUserCreatedCourseByCourseIdAsync(request.CourseId, cancellationToken);
        if (course == null)
        {
            throw new NotFoundException(nameof(UserCreatedCourse), request.CourseId);
        }
        
        var user = await unitOfWork.Users.FindUserByIdAsync(course.UserId);
        if (user == null)
        {
            throw new NotFoundException(nameof(UserCreatedCourse), request.CourseId);
        }

        return user.Email;
    }
}