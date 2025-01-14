using System.Reflection;
using CourseAppCourseService_Application.Common.Behavior;
using CourseAppCourseService_Application.Courses.Commands.CreateCourse;
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
        
        services.AddValidatorsFromAssemblyContaining<CreateCourseCommand>();
        services.AddTransient(typeof(IPipelineBehavior<,>),
            typeof(ValidationBehavior<,>));
        
        return services;
    }
}