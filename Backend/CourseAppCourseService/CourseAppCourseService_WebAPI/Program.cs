using CourseAppCourseService_Application.Interfaces;
using CourseAppCourseService_Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddPersistence(builder.Configuration);

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

app.UseHttpsRedirection();

app.Run();