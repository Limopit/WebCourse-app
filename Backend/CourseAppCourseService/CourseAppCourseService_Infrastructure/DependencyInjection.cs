using CourseAppCourseService_Application.Interfaces;
using CourseAppCourseService_Application.Interfaces.Repositories;
using CourseAppCourseService_Application.Interfaces.Services;
using CourseAppCourseService_Infrastructure.DbPattenrs;
using CourseAppCourseService_Infrastructure.DbPattenrs.Repositories;
using CourseAppCourseService_Infrastructure.Services;
using CourseAppCourseService_Infrastructure.Services.UserService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace CourseAppCourseService_Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var objectDiscriminatorConvention = BsonSerializer.LookupDiscriminatorConvention(typeof(object));
        var objectSerializer = new ObjectSerializer(objectDiscriminatorConvention, GuidRepresentation.Standard);
        BsonSerializer.RegisterSerializer(objectSerializer);
        BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
        
        var connectionString = configuration.GetConnectionString("DbConnection");
        var databaseName = configuration.GetValue<string>("MongoDbDatabaseName");
        var mongoUrl = new MongoUrl(connectionString);
        
        services.AddSingleton<IMongoClient>(new MongoClient(mongoUrl));
        services.AddScoped<ICourseDbContext>(serviceProvider =>
        {
            var client = serviceProvider.GetRequiredService<IMongoClient>();
            return new CourseDbContext(client, databaseName);
        });

        services.AddScoped<ICourseRepository, CourseRepository>();
        services.AddScoped<ILessonRepository, LessonRepository>();
        services.AddScoped<IQuizRepository, QuizRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IMapperService, MapperService>();
        services.AddTransient<GrpcUserServiceClient>();


        return services;
    }
}