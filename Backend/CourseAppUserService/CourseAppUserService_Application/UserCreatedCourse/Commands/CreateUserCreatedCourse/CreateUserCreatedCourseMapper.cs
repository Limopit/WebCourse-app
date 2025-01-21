using AutoMapper;
using CourseAppUserService_Application.Common.Mappings;
using CourseAppUserService_Domain.Entities;

namespace CourseAppUserService_Application.UserCreatedCourse.Commands.CreateUserCreatedCourse;

public class CreateUserCreatedCourseMapper: IMapWith<UserCreatedCourses>
{
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateUserCreatedCourseCommand, UserCreatedCourses>()
            .ForMember(course => course.CourseId, config => config.MapFrom(command => command.CourseId))
            .ForMember(course => course.ApprovementStatus, config => config.MapFrom(command => command.ApprovementStatus.ToString()));
    }
}