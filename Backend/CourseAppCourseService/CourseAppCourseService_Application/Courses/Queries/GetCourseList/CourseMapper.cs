using AutoMapper;
using CourseAppCourseService_Application.Common.Mappings;
using CourseAppCourseService_Domain;

namespace CourseAppCourseService_Application.Courses.Queries.GetCourseList;

public class CourseMapper : IMapWith<Course>
{
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Course, CourseDto>()
            .ForMember(dto => dto.Id, opt => opt.MapFrom(course => course.Id))
            .ForMember(dto => dto.Title, opt => opt.MapFrom(course => course.Title))
            .ForMember(dto => dto.Description, opt => opt.MapFrom(course => course.Description))
            .ForMember(dto => dto.Logo, opt => opt.MapFrom(course => course.Logo))
            .ForMember(dto => dto.Creator, opt => opt.MapFrom(course => course.Creator));

        profile.CreateMap<List<Course>, CourseVm>()
            .ForMember(vm => vm.Courses, opt => opt.MapFrom(list => list));
    }
}
