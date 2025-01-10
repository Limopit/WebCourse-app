using CourseAppUserService_IdentityServer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.Identity.Development.json", optional: true, reloadOnChange: true)
    .AddJsonFile("appsettings.Identity.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.Identity.{builder.Environment.EnvironmentName}.json", optional: true);

builder.Services.AddIdentityServer(options =>
    {
        options.EmitStaticAudienceClaim = true;
    })
    .AddInMemoryClients(IdentityServerConfig.GetClients())
    .AddInMemoryApiScopes(IdentityServerConfig.GetApiScopes())
    .AddDeveloperSigningCredential();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseIdentityServer();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();