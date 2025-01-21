using AutoMapper;
using CourseAppUserService_Application.Common.Mappings;
using CourseAppUserService_Domain.Entities;

namespace CourseAppUserService_Application.UserCreatedCourse.Queries.GetUserCreatedCourses;

public class UserCreatedCourseMapper : IMapWith<UserCreatedCourses>
{
    public void Mapping(Profile profile)
    {
        profile.CreateMap<UserCreatedCourses, UserCreatedCourseDto>()
            .ForMember(course => course.Id, config => config.MapFrom(command => command.CourseId))
            .ForMember(course => course.ApprovementStatus, config => config.MapFrom(command => command.ApprovementStatus))
            .ForMember(course => course.ApprovementDate, config => config.MapFrom(command => command.ApprovementDate));
    }
}