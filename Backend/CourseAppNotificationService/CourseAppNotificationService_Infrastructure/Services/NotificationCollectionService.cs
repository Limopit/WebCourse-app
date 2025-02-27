using CourseAppNotificationService_Domain.Interfaces.Services;
using Microsoft.Extensions.Hosting;

namespace CourseAppNotificationService_Infrastructure.Services;

public class NotificationCollectionService(IRabbitMqService rabbitMqService, INotificationService notificationService): IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        rabbitMqService.ConsumeAsync(async (notification) =>
        {
            await notificationService.SendNotificationToUserAsync(notification);
        });

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}