using CourseAppNotificationService_Domain.Interfaces.Repositories;
using CourseAppNotificationService_Domain.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using CourseAppNotificationService_Infrastructure.Repositories;
using CourseAppNotificationService_Infrastructure.Services;

namespace CourseAppNotificationService_Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var redisConnectionString = configuration.GetConnectionString("Redis");

        services.AddSingleton<IConnectionMultiplexer>(provider => ConnectionMultiplexer.Connect(redisConnectionString));

        services.AddSingleton<IDatabase>(sp =>
        {
            var connectionMultiplexer = sp.GetRequiredService<IConnectionMultiplexer>();
            return connectionMultiplexer.GetDatabase();
        });

        services.AddScoped<INotificationRepository, NotificationRepository>();
        services.AddSingleton<IRabbitMqService, RabbitMqService>();

        return services;
    }
}