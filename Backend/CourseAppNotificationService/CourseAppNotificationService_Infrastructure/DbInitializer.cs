using CourseAppNotificationService_Domain;
using CourseAppNotificationService_Domain.Interfaces.Repositories;
using StackExchange.Redis;

namespace CourseAppNotificationService_Infrastructure;

public static class DbInitializer
{
    public static async Task Initialize(IDatabase database, INotificationRepository notificationRepository)
    {
        await ClearOldNotificationsAsync(database);

        await SeedInitialNotificationsAsync(notificationRepository);
    }

    private static async Task ClearOldNotificationsAsync(IDatabase database)
    {
        var keys = database.Multiplexer.GetServer(database.Multiplexer.GetEndPoints().First())
            .Keys(pattern: "notifications:*");

        foreach (var key in keys)
        {
            await database.KeyDeleteAsync(key);
        }
    }

    private static async Task SeedInitialNotificationsAsync(INotificationRepository notificationRepository)
    {
        var initialNotifications = new List<Notification>
        {
            new Notification
            {
                Email = "admin@gmail.com",
                NotificationId = Guid.NewGuid(),
                Message = "Спасибо за регистрацию в нашем сервисе.",
                Timestamp = DateTime.UtcNow,
            },
            new Notification
            {
                Email = "user@gmail.com",
                NotificationId = Guid.NewGuid(),
                Message = "Мы выпустили новую версию приложения.",
                Timestamp = DateTime.UtcNow,
            }
        };

        foreach (var notification in initialNotifications)
        {
            await notificationRepository.AddNotificationAsync(notification);
        }
    }
}