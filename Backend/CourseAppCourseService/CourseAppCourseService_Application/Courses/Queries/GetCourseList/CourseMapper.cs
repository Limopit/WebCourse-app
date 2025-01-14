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
            .ForMember(courseDto => courseDto.CourseTitle, opt => opt.MapFrom(course => course.Title))
            .ForMember(courseDto => courseDto.CourseDescription, opt => opt.MapFrom(course => course.Description))
            .ForMember(courseDto => courseDto.CourseLogo, opt => opt.MapFrom(course => course.Logo))
            .ForMember(courseDto => courseDto.CourseCreator, opt => opt.MapFrom(course => course.Creator));

        profile.CreateMap<List<Course>, CourseVm>()
            .ForMember(courseVm => courseVm.Courses, opt => opt.MapFrom(courseList => courseList));
    }
}
