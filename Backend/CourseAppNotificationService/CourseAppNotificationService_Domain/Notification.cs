namespace CourseAppNotificationService_Domain;

public class Notification
{
    public Guid UserId { get; set; }
    public Guid NotificationId { get; set; }
    public string Title { get; set; }
    public string Message { get; set; }
    public DateTime Timestamp { get; set; }
    public bool IsRead { get; set; }
    public DateTime ActualTill { get; set; }
}