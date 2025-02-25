using CourseAppNotificationService_Domain;
using CourseAppNotificationService_Domain.Interfaces.Repositories;
using CourseAppNotificationService_Domain.Interfaces.Services;
using CourseAppNotificationService_Infrastructure.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace CourseAppNotificationService_Infrastructure.Services;

public class NotificationService(IHubContext<NotificationHub> context, INotificationRepository repository, IRabbitMqService rabbitMqService): INotificationService
{
    public async Task SendNotificationToUserAsync(Notification notification)
    {
        await repository.AddNotificationAsync(notification);

        if (NotificationHub.IsUserConnected(notification.Email))
        {
            Console.WriteLine($"{notification.Email} Notification sent");
            await context.Clients.User(notification.Email).SendAsync("ReceiveNotification", notification.Message);
        }
        else
        {
            Console.WriteLine($"{notification.Email} Notification published");
            await rabbitMqService.PublishAsync(notification);
        }
    }
    
    public async Task SendNotificationAsync(Notification notification)
    {
        await repository.AddNotificationAsync(notification);
        await context.Clients.All.SendAsync("ReceiveNotification", notification.Message);
    }
}