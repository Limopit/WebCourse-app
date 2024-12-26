namespace CourseAppUserService_Application.Interfaces.Services;

public interface IMapperService
{
    Task<TDestination> Map<TSource, TDestination>(TSource source);
    Task<TDestination> Update<TSource, TDestination>(TSource source, TDestination destination);
}