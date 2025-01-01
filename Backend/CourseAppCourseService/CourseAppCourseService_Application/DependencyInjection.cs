using System.Reflection;
using CourseAppCourseService_Application.Common.Behavior;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CourseAppCourseService_Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg
            =>cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        
        //services.AddValidatorsFromAssemblyContaining<RegisterUserCommand>();
        services.AddTransient(typeof(IPipelineBehavior<,>),
            typeof(ValidationBehavior<,>));
        
        return services;
    }
}