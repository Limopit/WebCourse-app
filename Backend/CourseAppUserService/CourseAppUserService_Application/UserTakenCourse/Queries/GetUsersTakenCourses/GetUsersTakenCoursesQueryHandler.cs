using CourseAppUserService_Application.Common.Exceptions;
using CourseAppUserService_Application.Interfaces;
using CourseAppUserService_Application.Interfaces.Services;
using CourseAppUserService_Domain;
using MediatR;

namespace CourseAppUserService_Application.UserTakenCourse.Queries.GetUsersTakenCourses;

public class GetUsersTakenCoursesQueryHandler(IUnitOfWork unitOfWork, IMapperService mapper)
    : IRequestHandler<GetUsersTakenCoursesQuery, UserTakenCourseVm>
{
    public async Task<UserTakenCourseVm> Handle(GetUsersTakenCoursesQuery request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.Users.FindUserByEmail(request.Email);
        
        if (user == null)
        {
            throw new NotFoundException(nameof(User), request.Email);
        }
        
        var courses = await unitOfWork.UserTakenCourses.GetUserTakenCoursesAsync(user.Id, cancellationToken);
        
        var userCourses = await mapper.Map<List<UserTakenCourses>, IList<UserTakenCourseDto>>(courses);
        
        return new UserTakenCourseVm { UserTakenCourses = userCourses };
    }
}