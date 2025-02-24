namespace CourseAppNotificationService_Domain.Interfaces.Services;

public interface IRabbitMqService
{
    Task PublishAsync(Notification notification);
    Task SubscribeAsync(Func<string, Task> handler);
}