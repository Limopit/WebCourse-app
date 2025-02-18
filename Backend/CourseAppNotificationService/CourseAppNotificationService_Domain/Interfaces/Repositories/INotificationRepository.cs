namespace CourseAppNotificationService_Domain.Interfaces.Repositories;

public interface INotificationRepository
{
    Task AddNotificationAsync(Notification notification);
    Task<Notification> GetNotificationAsync(Guid userId, Guid notificationId);
    Task<IEnumerable<Notification>> GetUserNotificationsAsync(Guid userId);
    Task MarkAsReadAsync(Guid userId, Guid notificationId);
    Task DeleteNotificationAsync(Guid userId, Guid notificationId);
}