using AutoMapper;
using CourseAppUserService_Application.Interfaces;
using CourseAppUserService_Application.Interfaces.Repositories;
using CourseAppUserService_Application.Interfaces.Services;
using CourseAppUserService_Domain;
using CourseAppUserService_Persistance.DbPatterns;
using CourseAppUserService_Persistance.DbPatterns.Repositories;
using CourseAppUserService_Persistance.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CourseAppUserService_Persistance;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistance(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DbConnection");

        services.AddDbContext<UserServiceDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        services.AddScoped<IUserServiceDbContext>(provider => provider.GetRequiredService<UserServiceDbContext>());

        services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 6;

                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<UserServiceDbContext>()
            .AddDefaultTokenProviders();
        
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserTakenCourseRepository, UserTakenCourseRepository>();
        services.AddScoped<IUserCreatedCourseRepository, UserCreatedCourseRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddTransient<IMapperService, MapperService>();
    
        return services;
    }

}