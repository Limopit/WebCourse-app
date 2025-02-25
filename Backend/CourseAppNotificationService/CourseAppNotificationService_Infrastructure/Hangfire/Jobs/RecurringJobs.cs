using CourseAppNotificationService_Domain;
using CourseAppNotificationService_Domain.Interfaces.Services;
using Hangfire;
using Microsoft.Extensions.DependencyInjection;

namespace CourseAppNotificationService_Infrastructure.Hangfire.Jobs;

public static class RecurringJobs
{
    public static void RegisterRecurringJobs(IServiceProvider serviceProvider)
    {
        var recurringJobManager = serviceProvider.GetRequiredService<IRecurringJobManager>();

        recurringJobManager.AddOrUpdate<INotificationService>(
            "scheduled-notification-admin-job",
            service => service.SendNotificationToUserAsync(new Notification 
            { 
                Email = "admin@gmail.com", 
                Message = "Scheduled notification for admin", 
                IsRead = false 
            }),
            Cron.Minutely
        );

        recurringJobManager.AddOrUpdate<INotificationService>(
            "scheduled-notification-user-job",
            service => service.SendNotificationToUserAsync(new Notification 
            { 
                Email = "user@gmail.com", 
                Message = "Scheduled notification for user", 
                IsRead = false 
            }),
            Cron.Minutely
        );
    }
}