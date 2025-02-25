namespace CourseAppNotificationService_Domain.Interfaces.Services;

public interface IRabbitMqService
{
    Task PublishAsync(Notification notification);
    Task<IEnumerable<Notification>> GetPendingNotificationsAsync(string email);
}