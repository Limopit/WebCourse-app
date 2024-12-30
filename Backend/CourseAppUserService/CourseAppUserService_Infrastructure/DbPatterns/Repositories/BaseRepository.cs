using CourseAppUserService_Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CourseAppUserService_Persistance.DbPatterns.Repositories;

public abstract class BaseRepository<T>: IBaseRepository<T> where T: class
{
    protected readonly UserServiceDbContext _dbContext;
    private readonly DbSet<T> _dbSet;

    protected BaseRepository(UserServiceDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<T>();
    }

    public async Task<T?> GetEntityByIdAsync(Guid id, CancellationToken token)
    {
        return await _dbSet.FindAsync(new object?[] { id }, token);
    }

    public async Task AddEntityAsync(T entity, CancellationToken token)
    {
        await _dbSet.AddAsync(entity, token);
    }
    
    public Task UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        return Task.CompletedTask;
    }

    public Task RemoveEntityAsync(T entity)
    {
        _dbSet.Remove(entity);
        return Task.CompletedTask;
    }
}