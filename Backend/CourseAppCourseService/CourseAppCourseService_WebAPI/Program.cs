using System.Reflection;
using CourseAppCourseService_Application;
using CourseAppCourseService_Application.Common.Mappings;
using CourseAppCourseService_Application.Interfaces;
using CourseAppCourseService_Infrastructure;
using CourseAppCourseService.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddApplication();

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
    config.AddProfile(new AssemblyMappingProfile(typeof(ICourseDbContext).Assembly));
});
builder.Services.AddPersistence(builder.Configuration);

builder.Services.AddSwaggerGen(); 
builder.Services.AddControllers();

builder.Services.AddGrpcClient<UserServiceRpc.UserService.UserServiceClient>(options =>
{
    options.Address = new Uri("https://localhost:7018");
})
.ConfigurePrimaryHttpMessageHandler(() =>
{
    var handler = new HttpClientHandler();
    
    // Отключаем проверку сертификатов (небезопасно для продакшн)
    handler.ServerCertificateCustomValidationCallback = 
        (sender, certificate, chain, sslPolicyErrors) => true;
    
    return handler;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    try
    {
        var context = serviceProvider.GetRequiredService<ICourseDbContext>();
        await DbInitializer.Initialize(context);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Произошла ошибка при инициализации БД: {ex.Message}");
    }
}

app.UseCustomExceptionHandler();
app.MapControllers();
app.UseHttpsRedirection();

app.Run();