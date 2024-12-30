namespace CourseAppUserService_Application.Interfaces.Services;

public interface IMapperService
{
    Task<TDestination> MapAsync<TSource, TDestination>(TSource source);
    Task<TDestination> UpdateAsync<TSource, TDestination>(TSource source, TDestination destination);
}