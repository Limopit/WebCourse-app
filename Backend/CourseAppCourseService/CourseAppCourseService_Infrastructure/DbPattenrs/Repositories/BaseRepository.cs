using CourseAppCourseService_Application.Interfaces;
using CourseAppCourseService_Application.Interfaces.Repositories;
using MongoDB.Driver;

namespace CourseAppCourseService_Infrastructure.DbPattenrs.Repositories;

public abstract class BaseRepository<T>(ICourseDbContext context, string collectionName) : IBaseRepository<T>
    where T : class
{
    private readonly IMongoCollection<T> _collection = context.Database.GetCollection<T>(collectionName);

    public async Task<T?> GetEntityByIdAsync(Guid id, CancellationToken token)
    {
        var filter = Builders<T>.Filter.Eq("_id", id);
        return await _collection.Find(filter).FirstOrDefaultAsync(token);
    }

    public async Task AddEntityAsync(T entity, CancellationToken token)
    {
        await _collection.InsertOneAsync(entity, cancellationToken: token);
    }

    public async Task UpdateAsync(T entity)
    {
        var filter = Builders<T>.Filter.Eq("_id", entity.GetType().GetProperty("Id")?.GetValue(entity).ToString());
        await _collection.ReplaceOneAsync(filter, entity);
    }

    public async Task RemoveEntityAsync(T entity)
    {
        var filter = Builders<T>.Filter.Eq("_id", entity.GetType().GetProperty("Id")?.GetValue(entity).ToString());
        await _collection.DeleteOneAsync(filter);
    }

    public async Task<List<T>> GetAllEntitiesAsync(CancellationToken token)
    {
        return await _collection.Find(Builders<T>.Filter.Empty)
            .ToListAsync(token);
    }
}