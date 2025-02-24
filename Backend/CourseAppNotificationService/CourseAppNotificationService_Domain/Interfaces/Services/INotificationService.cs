namespace CourseAppNotificationService_Domain.Interfaces.Services;

public interface INotificationService
{
    Task SendNotificationToUserAsync(Notification notification);

    Task SendNotificationAsync(Notification notification);
}