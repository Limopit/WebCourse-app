using AutoMapper;

namespace CourseAppCourseService_Application.Common.Mappings;

public interface IMapWith<T>
{
    void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
}