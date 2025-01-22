using AutoMapper;
using CourseAppUserService_Application.Common.Mappings;
using CourseAppUserService_Domain.Entities;

namespace CourseAppUserService_Application.UserTakenCourse.Queries.GetUsersTakenCourses;

public class UserTakenCourseMapper : IMapWith<UserTakenCourses>
{
    public void Mapping(Profile profile)
    {
        profile.CreateMap<UserTakenCourses, UserTakenCourseDto>()
            .ForMember(dto => dto.Id, config => config.MapFrom(course => course.CourseId))
            .ForMember(dto => dto.StartDate, config => config.MapFrom(course => course.DateStart))
            .ForMember(dto => dto.FinishDate, config => config.MapFrom(course => course.DateFinished))
            .ForMember(dto => dto.Status, config => config.MapFrom(course => course.Status))
            .ForMember(dto => dto.IsFavorite, config => config.MapFrom(course => course.IsFavourite));
    }
}