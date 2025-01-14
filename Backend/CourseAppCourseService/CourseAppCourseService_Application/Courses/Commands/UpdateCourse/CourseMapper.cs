using AutoMapper;
using CourseAppCourseService_Application.Common.Mappings;
using CourseAppCourseService_Domain;

namespace CourseAppCourseService_Application.Courses.Commands.UpdateCourse;

public class CourseMapper: IMapWith<Course>
{
    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateCourseCommand, Course>()
            .ForMember(course => course.Title, opt => opt.MapFrom(command => command.CourseTitle))
            .ForMember(course => course.Description, opt => opt.MapFrom(command => command.CourseDescription))
            .ForMember(course => course.Logo, opt => opt.MapFrom(command => command.CourseLogo))
            .ForMember(course => course.Level, opt => opt.MapFrom(command => command.CourseLevel))
            .ForMember(course => course.Category, opt => opt.MapFrom(command => command.CourseCategory))
            .ForMember(course => course.Language, opt => opt.MapFrom(command => command.CourseLanguage))
            .ForMember(course => course.Requierments, opt => opt.MapFrom(command => command.CourseRequierments));
    }
}