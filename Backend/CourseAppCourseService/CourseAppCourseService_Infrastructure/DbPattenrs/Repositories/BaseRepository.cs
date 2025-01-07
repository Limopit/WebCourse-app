using CourseAppCourseService_Application.Interfaces;
using CourseAppCourseService_Application.Interfaces.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CourseAppCourseService_Infrastructure.DbPattenrs.Repositories;

public abstract class BaseRepository<T>(ICourseDbContext context, string collectionName) : IBaseRepository<T>
    where T : class
{
    protected readonly IMongoCollection<T> Collection = context.Database.GetCollection<T>(collectionName);

    public async Task<T?> GetEntityByIdAsync(Guid id, CancellationToken token)
    {
        var filter = Builders<T>.Filter.Eq("_id", id);
        return await Collection.Find(filter).FirstOrDefaultAsync(token);
    }


    public async Task AddEntityAsync(T entity, CancellationToken token)
    {
        await Collection.InsertOneAsync(entity, cancellationToken: token);
    }

    public async Task UpdateAsync(T entity)
    {
        var filter = Builders<T>.Filter.Eq("_id", entity.GetType().GetProperty("Id")?.GetValue(entity).ToString());
        await Collection.ReplaceOneAsync(filter, entity);
    }

    public async Task RemoveEntityAsync(T entity)
    {
        var filter = Builders<T>.Filter.Eq("_id", entity.GetType().GetProperty("Id")?.GetValue(entity).ToString());
        await Collection.DeleteOneAsync(filter);
    }

    public async Task<List<T>> GetAllEntitiesAsync(CancellationToken token)
    {
        return await Collection.Find(Builders<T>.Filter.Empty)
            .ToListAsync(token);
    }
}