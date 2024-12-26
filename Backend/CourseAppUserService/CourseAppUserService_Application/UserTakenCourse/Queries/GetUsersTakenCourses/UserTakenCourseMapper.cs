using AutoMapper;
using CourseAppUserService_Application.Common.Mappings;
using CourseAppUserService_Domain;

namespace CourseAppUserService_Application.UserTakenCourse.Queries.GetUsersTakenCourses;

public class UserTakenCourseMapper : IMapWith<UserTakenCourses>
{
    public void Mapping(Profile profile)
    {
        profile.CreateMap<UserTakenCourses, UserTakenCourseDto>()
            .ForMember(course => course.CourseId, opt => opt.MapFrom(userCourse => userCourse.CourseId))
            .ForMember(course => course.StartDate, opt => opt.MapFrom(userCourse => userCourse.DateStart))
            .ForMember(course => course.FinishDate, opt => opt.MapFrom(userCourse => userCourse.DateFinished))
            .ForMember(course => course.Status, opt => opt.MapFrom(userCourse => userCourse.Status))
            .ForMember(course => course.IsFavorite, opt => opt.MapFrom(userCourse => userCourse.IsFavourite));
    }
}