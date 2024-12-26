using AutoMapper;
using CourseAppUserService_Application.Common.Mappings;
using CourseAppUserService_Application.UserTakenCourse.Queries.GetUsersTakenCourses;
using CourseAppUserService_Domain;

namespace CourseAppUserService_Application.UserCreatedCourse.Queries.GetUserCreatedCourses;

public class UserCreatedCourseMapper : IMapWith<UserCreatedCourses>
{
    public void Mapping(Profile profile)
    {
        profile.CreateMap<UserCreatedCourses, UserCreatedCourseDto>()
            .ForMember(course => course.CourseId, opt => opt.MapFrom(userCourse => userCourse.CourseId))
            .ForMember(course => course.ApprovementStatus, opt => opt.MapFrom(userCourse => userCourse.ApprovementStatus))
            .ForMember(course => course.ApprovementDate, opt => opt.MapFrom(userCourse => userCourse.ApprovementDate));
    }
}