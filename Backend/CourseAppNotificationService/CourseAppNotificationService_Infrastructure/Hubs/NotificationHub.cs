using System.Collections.Concurrent;
using CourseAppNotificationService_Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace CourseAppNotificationService_Infrastructure.Hubs;

[Authorize]
public class NotificationHub(IRabbitMqService rabbitMqService) : Hub
{
    private static readonly ConcurrentDictionary<string, string> ConnectedUsers = new();

    public override async Task OnConnectedAsync()
    {
        var email = Context.User?.Identity?.Name;
        if (!string.IsNullOrEmpty(email))
        {
            ConnectedUsers.TryAdd(email, Context.ConnectionId);
            Console.WriteLine($"User {email} connected. Connection ID: {Context.ConnectionId}");

            await SendPendingNotificationsAsync(email);
        }
        else
        {
            Console.WriteLine("User is not authenticated.");
        }
        await base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception exception)
    {
        var email = Context.User?.Identity?.Name;
        if (!string.IsNullOrEmpty(email))
        {
            ConnectedUsers.TryRemove(email, out _);
            Console.WriteLine($"User {email} disconnected.");
        }
        return base.OnDisconnectedAsync(exception);
    }

    public static bool IsUserConnected(string email)
    {
        return ConnectedUsers.ContainsKey(email);
    }

    private async Task SendPendingNotificationsAsync(string email)
    {
        var notifications = await rabbitMqService.GetPendingNotificationsAsync(email);
        Console.WriteLine(notifications.Count());
        foreach (var notification in notifications)
        {
            await Clients.User(email).SendAsync("ReceiveNotification", notification);
            Console.WriteLine($"Pending notification sent to {email}: {notification.Message}");
        }
    }
}