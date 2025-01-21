using AutoMapper;
using CourseAppCourseService_Application.Common.Mappings;
using CourseAppCourseService_Domain;

namespace CourseAppCourseService_Application.Courses.Commands.CreateCourse;

public class CourseMapper: IMapWith<Course>
{
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateCourseCommand, Course>()
            .ForMember(course => course.Title, opt => opt.MapFrom(command => command.Title))
            .ForMember(course => course.Description, opt => opt.MapFrom(command => command.Description))
            .ForMember(course => course.Logo, opt => opt.MapFrom(command => command.Logo))
            .ForMember(course => course.Level, opt => opt.MapFrom(command => command.Level))
            .ForMember(course => course.Category, opt => opt.MapFrom(command => command.Category))
            .ForMember(course => course.Language, opt => opt.MapFrom(command => command.Language))
            .ForMember(course => course.Requierments, opt => opt.MapFrom(command => command.Requierments));
    }
}