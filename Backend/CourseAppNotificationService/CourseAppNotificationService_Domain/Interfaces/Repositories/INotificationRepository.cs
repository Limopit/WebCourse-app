namespace CourseAppNotificationService_Domain.Interfaces.Repositories;

public interface INotificationRepository
{
    Task AddNotificationAsync(Notification notification);
    Task<Notification> GetNotificationAsync(string email, Guid notificationId);
    Task<IEnumerable<Notification>> GetUserNotificationsAsync(string email);
    Task MarkAsReadAsync(string email, Guid notificationId);
    Task DeleteNotificationAsync(string email, Guid notificationId);
}