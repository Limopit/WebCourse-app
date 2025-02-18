using AutoMapper;
using CourseAppCourseService_Application.Common.Mappings;
using CourseAppCourseService_Domain;

namespace CourseAppCourseService_Application.Courses.Queries.GetCourseById;

public class CourseMapper : IMapWith<Course>
{
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Course, CourseDto>();
        profile.CreateMap<Lesson, LessonDto>();
        profile.CreateMap<Quiz, QuizDto>();
    }
}