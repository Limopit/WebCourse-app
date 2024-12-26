using System.Reflection;
using CourseAppUserService_Application.Common.Behavior;
using CourseAppUserService_Application.Users.Commands.RegisterUser;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CourseAppUserService_Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg
            =>cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        
        services.AddValidatorsFromAssemblyContaining<RegisterUserCommand>();
        services.AddTransient(typeof(IPipelineBehavior<,>),
            typeof(ValidationBehavior<,>));
        
        return services;
    }
}