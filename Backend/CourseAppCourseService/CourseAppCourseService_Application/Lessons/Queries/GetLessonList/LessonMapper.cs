using AutoMapper;
using CourseAppCourseService_Application.Common.Mappings;
using CourseAppCourseService_Domain;

namespace CourseAppCourseService_Application.Lessons.Queries.GetLessonList;

public class LessonMapper : IMapWith<Lesson>
{
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Lesson, LessonDto>()
            .ForMember(dto => dto.Id, opt => opt.MapFrom(lesson => lesson.Id))
            .ForMember(dto => dto.LessonTitle, opt => opt.MapFrom(lesson => lesson.Title))
            .ForMember(dto => dto.LessonDescription, opt => opt.MapFrom(lesson => lesson.Description));

        profile.CreateMap<List<Lesson>, LessonVm>()
            .ForMember(vm => vm.Lessons, opt => opt.MapFrom(lessonList => lessonList));
    }
}
