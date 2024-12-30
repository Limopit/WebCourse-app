using AutoMapper;
using CourseAppUserService_Application.Common.Mappings;
using CourseAppUserService_Domain.Entities;

namespace CourseAppUserService_Application.UserTakenCourse.Queries.GetUsersTakenCourses;

public class UserTakenCourseMapper : IMapWith<UserTakenCourses>
{
    public void Mapping(Profile profile)
    {
        profile.CreateMap<UserTakenCourses, UserTakenCourseDto>()
            .ForMember(course => course.CourseId, config => config.MapFrom(userCourse => userCourse.CourseId))
            .ForMember(course => course.StartDate, config => config.MapFrom(userCourse => userCourse.DateStart))
            .ForMember(course => course.FinishDate, config => config.MapFrom(userCourse => userCourse.DateFinished))
            .ForMember(course => course.Status, config => config.MapFrom(userCourse => userCourse.Status))
            .ForMember(course => course.IsFavorite, config => config.MapFrom(userCourse => userCourse.IsFavourite));
    }
}