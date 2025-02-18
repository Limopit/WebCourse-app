using CourseAppNotificationService_Domain;
using StackExchange.Redis;
using System.Text.Json;
using CourseAppNotificationService_Domain.Interfaces.Repositories;

namespace CourseAppNotificationService_Infrastructure.Repositories;

public class NotificationRepository(IDatabase database) : INotificationRepository
{
    public async Task AddNotificationAsync(Notification notification)
    {
        var key = $"notifications:{notification.UserId}:{notification.NotificationId}";
        var value = JsonSerializer.Serialize(notification);
        await database.StringSetAsync(key, value);
    }

    public async Task<Notification> GetNotificationAsync(Guid userId, Guid notificationId)
    {
        var key = $"notifications:{userId}:{notificationId}";
        var value = await database.StringGetAsync(key);
        return value.HasValue ? JsonSerializer.Deserialize<Notification>(value) : null;
    }

    public async Task<IEnumerable<Notification>> GetUserNotificationsAsync(Guid userId)
    {
        var pattern = $"notifications:{userId}:*";
        var keys = database.Multiplexer.GetServer(database.Multiplexer.GetEndPoints().First()).Keys(pattern: pattern);

        var notifications = new List<Notification>();
        foreach (var key in keys)
        {
            var value = await database.StringGetAsync(key);
            if (value.HasValue)
            {
                notifications.Add(JsonSerializer.Deserialize<Notification>(value));
            }
        }

        return notifications;
    }

    public async Task MarkAsReadAsync(Guid userId, Guid notificationId)
    {
        var key = $"notifications:{userId}:{notificationId}";
        var value = await database.StringGetAsync(key);

        if (value.HasValue)
        {
            var notification = JsonSerializer.Deserialize<Notification>(value);
            notification.IsRead = true;
            await database.StringSetAsync(key, JsonSerializer.Serialize(notification));
        }
    }

    public async Task DeleteNotificationAsync(Guid userId, Guid notificationId)
    {
        var key = $"notifications:{userId}:{notificationId}";
        await database.KeyDeleteAsync(key);
    }
}