using AutoMapper;
using CourseAppUserService_Application.Common.Mappings;
using CourseAppUserService_Domain;

namespace CourseAppUserService_Application.UserTakenCourse.Commands.CreateUserTakenCourse;

public class CreateUserTakenCourseMapper: IMapWith<UserTakenCourses>
{
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateUserTakenCourseCommand, UserTakenCourses>()
            .ForMember(course => course.CourseId, opt => opt.MapFrom(userCourse => userCourse.CourseId))
            .ForMember(course => course.DateStart, opt => opt.MapFrom(userCourse => userCourse.StartDate))
            .ForMember(course => course.Status, opt => opt.MapFrom(userCourse => userCourse.Status));
    }
}