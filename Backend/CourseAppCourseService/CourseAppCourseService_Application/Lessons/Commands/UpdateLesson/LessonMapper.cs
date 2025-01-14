using AutoMapper;
using CourseAppCourseService_Application.Common.Mappings;
using CourseAppCourseService_Domain;

namespace CourseAppCourseService_Application.Lessons.Commands.UpdateLesson;

public class LessonMapper: IMapWith<Lesson>
{
    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateLessonCommand, Lesson>()
            .ForMember(lesson => lesson.Id, opt => opt.MapFrom(command => command.Id))
            .ForMember(lesson => lesson.Title, opt => opt.MapFrom(command => command.LessonTitle))
            .ForMember(lesson => lesson.Description, opt => opt.MapFrom(command => command.LessonDescription))
            .ForMember(lesson => lesson.Type, opt => opt.MapFrom(command => command.LessonType))
            .ForMember(lesson => lesson.Duration, opt => opt.MapFrom(command => command.LessonDuration))
            .ForMember(lesson => lesson.Content, opt => opt.MapFrom(command => command.LessonContent));
    }
}