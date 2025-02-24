using System.Text;
using CourseAppNotificationService_Domain.Interfaces.Repositories;
using CourseAppNotificationService_Domain.Interfaces.Services;
using CourseAppNotificationService_Infrastructure;
using CourseAppNotificationService_Infrastructure.Hangfire;
using CourseAppNotificationService_Infrastructure.Hangfire.Jobs;
using CourseAppNotificationService_Infrastructure.Hubs;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("https://localhost:3000")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    RecurringJobs.RegisterRecurringJobs(scope.ServiceProvider);
}

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

app.MapHub<NotificationHub>("/notificationHub");

app.UseCors("AllowFrontend");

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.UseHangfireDashboard("/hangfire", new DashboardOptions
{
    Authorization = new[] { new HangfireDashboardAuthorization() }
});

app.Run();