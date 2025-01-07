using AutoMapper;
using CourseAppCourseService_Application.Common.Mappings;
using CourseAppCourseService_Domain;

namespace CourseAppCourseService_Application.Lessons.Commands.CreateLesson;

public class LessonMapper: IMapWith<Lesson>
{
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateLessonCommand, Lesson>()
            .ForMember(lesson => lesson.LessonTitle, opt => opt.MapFrom(command => command.LessonTitle))
            .ForMember(lesson => lesson.LessonDescription, opt => opt.MapFrom(command => command.LessonDescription))
            .ForMember(lesson => lesson.LessonType, opt => opt.MapFrom(command => command.LessonType))
            .ForMember(lesson => lesson.LessonDuration, opt => opt.MapFrom(command => command.LessonDuration))
            .ForMember(lesson => lesson.LessonContent, opt => opt.MapFrom(command => command.LessonContent));
    }
}