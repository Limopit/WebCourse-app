namespace CourseAppCourseService_Application.Interfaces.Repositories;

public interface IBaseRepository<T> where T: class
{
    Task<T?> GetEntityByIdAsync(Guid id, CancellationToken token);
    Task AddEntityAsync(T entity, CancellationToken token);
    Task UpdateAsync(T entity);
    Task RemoveEntityAsync(T entity);
}