namespace CourseAppNotificationService_Domain;

public class Notification
{
    public Guid NotificationId { get; set; }
    public string Email { get; set; }
    public string Message { get; set; }
    public DateTime Timestamp { get; set; }
}