using AutoMapper;
using CourseAppUserService_Application.Common.Mappings;
using CourseAppUserService_Domain;

namespace CourseAppUserService_Application.UserCreatedCourse.Queries.GetUserCreatedCourses;

public class UserCreatedCourseMapper : IMapWith<UserCreatedCourses>
{
    public void Mapping(Profile profile)
    {
        profile.CreateMap<UserCreatedCourses, UserCreatedCourseDto>()
            .ForMember(course => course.CourseId, config => config.MapFrom(userCourse => userCourse.CourseId))
            .ForMember(course => course.ApprovementStatus, config => config.MapFrom(userCourse => userCourse.ApprovementStatus))
            .ForMember(course => course.ApprovementDate, config => config.MapFrom(userCourse => userCourse.ApprovementDate));
    }
}