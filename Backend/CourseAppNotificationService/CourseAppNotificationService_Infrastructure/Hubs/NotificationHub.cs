using Microsoft.AspNetCore.SignalR;

namespace CourseAppNotificationService_Infrastructure.Hubs;

public class NotificationHub: Hub
{
    public async Task SendNotificationToUserAsync(string email, string message)
    {
        await Clients.User(email).SendAsync("ReceiveNotification", message);
    }
    
    public async Task SendNotificationAsync(string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", message);
    }
}