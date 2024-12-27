using AutoMapper;
using CourseAppUserService_Application.Common.Mappings;
using CourseAppUserService_Domain;

namespace CourseAppUserService_Application.UserCreatedCourse.Commands.CreateUserCreatedCourse;

public class CreateUserCreatedCourseMapper: IMapWith<UserCreatedCourses>
{
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateUserCreatedCourseCommand, UserCreatedCourses>()
            .ForMember(course => course.CourseId, config => config.MapFrom(userCourse => userCourse.CourseId))
            .ForMember(dest => dest.ApprovementStatus, config => config.MapFrom(command => command.ApprovementStatus.ToString()));
    }
}