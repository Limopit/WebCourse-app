using CourseAppNotificationService_Domain.Interfaces.Repositories;
using CourseAppNotificationService_Domain.Interfaces.Services;
using CourseAppNotificationService_Infrastructure;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Notification Service API", Version = "v1" });
});

builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var database = scope.ServiceProvider.GetRequiredService<IDatabase>();
    var notificationRepository = scope.ServiceProvider.GetRequiredService<INotificationRepository>();

    await DbInitializer.Initialize(database, notificationRepository);
}

using (var scope = app.Services.CreateScope())
{
    var messageQueueService = scope.ServiceProvider.GetRequiredService<IRabbitMqService>();
    await messageQueueService.SubscribeAsync(async message =>
    {
        Console.WriteLine($"Received message: {message}");
        await Task.CompletedTask;
    });
}

using (var scope = app.Services.CreateScope())
{
    var messageQueueService = scope.ServiceProvider.GetRequiredService<IRabbitMqService>();
    await messageQueueService.PublishAsync("Hello, RabbitMQ!");
}


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Notification Service API v1");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();