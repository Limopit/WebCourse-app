using AutoMapper;
using CourseAppUserService_Application.Common.Mappings;
using CourseAppUserService_Application.UserTakenCourse.Commands.CreateUserTakenCourse;
using CourseAppUserService_Domain;

namespace CourseAppUserService_Application.UserCreatedCourse.Commands.CreateUserCreatedCourse;

public class CreateUserCreatedCourseMapper: IMapWith<UserCreatedCourses>
{
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateUserCreatedCourseCommand, UserCreatedCourses>()
            .ForMember(course => course.CourseId, opt => opt.MapFrom(userCourse => userCourse.CourseId))
            .ForMember(course => course.ApprovementStatus, opt => opt.MapFrom(userCourse => userCourse.ApprovementStatus));
    }
}