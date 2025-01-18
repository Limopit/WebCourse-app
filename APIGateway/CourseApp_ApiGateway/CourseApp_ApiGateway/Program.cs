using MMLib.SwaggerForOcelot.DependencyInjection;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Configuration.AddOcelotWithSwaggerSupport((o) =>
{
    o.Folder = $"OcelotConfig/{builder.Environment.EnvironmentName}";
    o.FileOfSwaggerEndPoints = "ocelot.swagger";
});

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = "https://localhost:5001";
        options.RequireHttpsMetadata = false;
        options.Audience = "api_scope";
    });


builder.Services.AddOcelot();
builder.Services.AddSwaggerForOcelot(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

app.UseSwaggerForOcelotUI(options =>
{
    options.PathToSwaggerGenerator = "/swagger/docs";
}).UseOcelot().Wait();

app.Run();