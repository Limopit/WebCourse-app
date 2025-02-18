namespace CourseAppNotificationService_Domain.Interfaces.Services;

public interface IRabbitMqService
{
    Task PublishAsync(string message);
    Task SubscribeAsync(Func<string, Task> handler);
}