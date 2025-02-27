using CourseAppUserService_Application.Interfaces.Services;
using CourseAppUserService_Domain.Entities;

namespace CourseAppUserService_Persistance.Services;

public class NotificationService(IRabbitMqService rabbitMqService): INotificationService
{
    public async Task PerformAction(string email, string message)
    {
        var notification = new Notification
        {
            NotificationId = Guid.NewGuid(),
            Email = email,
            Message = message,
            Timestamp = DateTime.UtcNow,
            Sourse = "User"
        };

        await rabbitMqService.PublishAsync(notification);

        Console.WriteLine($"Action performed and notification published for {email}");
    }
}