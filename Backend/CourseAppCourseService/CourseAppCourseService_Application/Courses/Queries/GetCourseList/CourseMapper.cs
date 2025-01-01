using AutoMapper;
using CourseAppCourseService_Application.Common.Mappings;
using CourseAppCourseService_Domain;

namespace CourseAppCourseService_Application.Courses.Queries.GetCourseList;

public class CourseMapper : IMapWith<Course>
{
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Course, CourseDto>()
            .ForMember(courseDto => courseDto.Id, opt => opt.MapFrom(course => course.Id))
            .ForMember(courseDto => courseDto.CourseTitle, opt => opt.MapFrom(course => course.CourseTitle))
            .ForMember(courseDto => courseDto.CourseDescription, opt => opt.MapFrom(course => course.CourseDescription))
            .ForMember(courseDto => courseDto.CourseLogo, opt => opt.MapFrom(course => course.CourseLogo))
            .ForMember(courseDto => courseDto.CourseCreator, opt => opt.MapFrom(course => course.CourseCreator));

        profile.CreateMap<List<Course>, CourseVm>()
            .ForMember(courseVm => courseVm.Courses, opt => opt.MapFrom(courseList => courseList));
    }
}
