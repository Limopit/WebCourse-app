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
                Title = "Добро пожаловать!",
                Message = "Спасибо за регистрацию в нашем сервисе.",
                Timestamp = DateTime.UtcNow,
                IsRead = false,
                ActualTill = DateTime.UtcNow.AddDays(7)
            },
            new Notification
            {
                Email = "user@gmail.com",
                NotificationId = Guid.NewGuid(),
                Title = "Важное обновление",
                Message = "Мы выпустили новую версию приложения.",
                Timestamp = DateTime.UtcNow,
                IsRead = false,
                ActualTill = DateTime.UtcNow.AddDays(5)
            }
        };

        foreach (var notification in initialNotifications)
        {
            await notificationRepository.AddNotificationAsync(notification);
        }
    }
}