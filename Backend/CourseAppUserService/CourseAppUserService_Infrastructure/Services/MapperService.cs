using AutoMapper;
using CourseAppUserService_Application.Interfaces.Services;

namespace CourseAppUserService_Persistance.Services;

public class MapperService(IMapper mapper) : IMapperService
{
    public async Task<TDestination> MapAsync<TSource, TDestination>(TSource source)
    {
        var result = await Task.Run(() => mapper.Map<TDestination>(source)); 
        return result;
    }
    
    public async Task<TDestination> UpdateAsync<TSource, TDestination>(TSource source, TDestination destination)
    {
        var result = await Task.Run(() =>  mapper.Map(source, destination));
        return result;
    }
}