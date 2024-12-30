using CourseAppCourseService_Application.Interfaces;
using CourseAppCourseService_Infrastructure.DbPattenrs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace CourseAppCourseService_Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DbConnection");
        var databaseName = configuration.GetValue<string>("MongoDbDatabaseName");

        var mongoUrl = new MongoUrl(connectionString);
        services.AddSingleton<IMongoClient>(new MongoClient(mongoUrl));

        services.AddScoped<ICourseDbContext>(serviceProvider =>
        {
            var client = serviceProvider.GetRequiredService<IMongoClient>();
            return new CourseDbContext(client, databaseName);
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}