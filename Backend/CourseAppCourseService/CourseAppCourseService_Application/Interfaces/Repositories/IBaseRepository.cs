namespace CourseAppCourseService_Application.Interfaces.Repositories;

public interface IBaseRepository<T> where T: class
{
    Task<T?> GetEntityByIdAsync(Guid id, CancellationToken token);
    Task<List<T>> GetEntityListInfoByIdAsync(List<Guid> ids, int pageNumber, int pageSize, CancellationToken token);
    Task AddEntityAsync(T entity, CancellationToken token);
    Task UpdateAsync(T entity);
    Task RemoveEntityAsync(T entity);
    Task<List<T>> GetAllEntitiesAsync(CancellationToken token);
}