using CourseAppUserService_Application.Common.Exceptions;
using CourseAppUserService_Application.Interfaces;
using CourseAppUserService_Application.Interfaces.Services;
using CourseAppUserService_Domain.Entities;
using MediatR;

namespace CourseAppUserService_Application.UserTakenCourse.Queries.GetUsersTakenCourses;

public class GetUsersTakenCoursesQueryHandler(IUnitOfWork unitOfWork, IMapperService mapper)
    : IRequestHandler<GetUsersTakenCoursesQuery, UserTakenCourseVm>
{
    public async Task<UserTakenCourseVm> Handle(GetUsersTakenCoursesQuery request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.Users.FindUserByEmailAsync(request.Email);
        
        if (user == null)
        {
            throw new NotFoundException(nameof(User), request.Email);
        }
        
        var courses = await unitOfWork.UserTakenCourses.GetUserTakenCoursesAsync(user.Id, cancellationToken);
        
        var userCourses = await mapper.MapAsync<List<UserTakenCourses>, IList<UserTakenCourseDto>>(courses);
        
        return new UserTakenCourseVm { UserTakenCourses = userCourses };
    }
}