using CourseAppCourseService_Application.Interfaces.Repositories;
using MongoDB.Driver;

namespace CourseAppCourseService_Infrastructure.DbPattenrs.Repositories;

public abstract class BaseRepository<T>(IMongoDatabase database, string collectionName) : IBaseRepository<T>
    where T : class
{
    private readonly IMongoCollection<T> _collection = database.GetCollection<T>(collectionName);

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
        var filter = Builders<T>.Filter.Eq("_id", entity.GetType().GetProperty("Id")?.GetValue(entity));
        await _collection.ReplaceOneAsync(filter, entity);
    }

    public async Task RemoveEntityAsync(T entity)
    {
        var filter = Builders<T>.Filter.Eq("_id", entity.GetType().GetProperty("Id")?.GetValue(entity));
        await _collection.DeleteOneAsync(filter);
    }
}