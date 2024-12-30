using CourseAppUserService_Application.Common.Exceptions;
using CourseAppUserService_Application.Interfaces;
using CourseAppUserService_Application.Interfaces.Services;
using CourseAppUserService_Domain.Entities;
using MediatR;

namespace CourseAppUserService_Application.UserCreatedCourse.Queries.GetUserCreatedCourses;

public class GetUserCreatedCoursesQueryHandler(IUnitOfWork unitOfWork, IMapperService mapper): IRequestHandler<GetUserCreatedCoursesQuery, UserCreatedCourseVm>
{
    public async Task<UserCreatedCourseVm> Handle(GetUserCreatedCoursesQuery request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.Users.FindUserByEmailAsync(request.Email);
        
        if (user == null)
        {
            throw new NotFoundException(nameof(User), request.Email);
        }
        
        var courses = await unitOfWork.UserCreatedCourses.GetUserCreatedCoursesAsync(user.Id, cancellationToken);
        
        var userCourses = await mapper.MapAsync<List<UserCreatedCourses>, IList<UserCreatedCourseDto>>(courses);
        
        return new UserCreatedCourseVm() { UserCreatedCourses = userCourses };
    }
}