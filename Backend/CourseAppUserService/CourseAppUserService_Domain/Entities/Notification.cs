namespace CourseAppUserService_Domain.Entities;

public class Notification
{
    public Guid NotificationId { get; set; }
    public string Email { get; set; }
    public string Message { get; set; }
    public DateTime Timestamp { get; set; }
    public string Sourse { get; set; }
}