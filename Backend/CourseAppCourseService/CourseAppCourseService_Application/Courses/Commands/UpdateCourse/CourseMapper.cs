using AutoMapper;
using CourseAppCourseService_Application.Common.Mappings;
using CourseAppCourseService_Domain;

namespace CourseAppCourseService_Application.Courses.Commands.UpdateCourse;

public class CourseMapper: IMapWith<Course>
{
    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateCourseCommand, Course>()
            .ForMember(course => course.CourseTitle, opt => opt.MapFrom(command => command.CourseTitle))
            .ForMember(course => course.CourseDescription, opt => opt.MapFrom(command => command.CourseDescription))
            .ForMember(course => course.CourseLogo, opt => opt.MapFrom(command => command.CourseLogo))
            .ForMember(course => course.CourseLevel, opt => opt.MapFrom(command => command.CourseLevel))
            .ForMember(course => course.CourseCategory, opt => opt.MapFrom(command => command.CourseCategory))
            .ForMember(course => course.CourseLanguage, opt => opt.MapFrom(command => command.CourseLanguage))
            .ForMember(course => course.CourseRequierments, opt => opt.MapFrom(command => command.CourseRequierments));
    }
}