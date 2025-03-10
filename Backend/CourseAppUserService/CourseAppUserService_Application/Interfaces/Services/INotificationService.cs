namespace CourseAppUserService_Application.Interfaces.Services;

public interface INotificationService
{
    Task PerformAction(string email, string message);
}