using System.Text;
using CourseAppIntegrationTests.Fixtures;
using CourseAppIntegrationTests.Fixtures.NotificationService;
using FluentAssertions;
using RabbitMQ.Client;
using StackExchange.Redis;
using Xunit;
using Xunit.Abstractions;

namespace CourseAppIntegrationTests.ContainerTests;

public class NotificationTests(NotificationServiceWithDbFixture fixture, ITestOutputHelper testOutputHelper): IClassFixture<NotificationServiceWithDbFixture>
{
    private const string QueueName = "notifications";
    private readonly ConnectionFactory _factory = new()
    {
        HostName = "localhost",
        Port = 5672,
        UserName = "admin",
        Password = "admin"
    };

    [Fact]
    public async Task Queue_Should_Exist_And_Have_Messages()
    {
        // Arrange
        using var connection = await _factory.CreateConnectionAsync();
        using var channel = await connection.CreateChannelAsync();

        // Act & Assert
        await Task.Delay(TimeSpan.FromMinutes(2));

        var queueDeclareOk = await channel.QueueDeclarePassiveAsync(QueueName);
        queueDeclareOk.Should().NotBeNull($"Очередь {QueueName} должна существовать.");

        uint messageCount = queueDeclareOk.MessageCount;
        messageCount.Should().BeGreaterThan(0, $"Ожидалось хотя бы одно сообщение в очереди {QueueName}.");

        testOutputHelper.WriteLine($"Очередь {QueueName} существует и содержит {messageCount} сообщений.");

        for (uint i = 0; i < messageCount; i++)
        {
            var result =
                await channel.BasicGetAsync(QueueName, autoAck: false);
            if (result != null)
            {
                var body = result.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                testOutputHelper.WriteLine($"Сообщение {i + 1}: {message}");

                await channel.BasicNackAsync(result.DeliveryTag, multiple: false, requeue: true);
            }
        }
    }
    
    [Fact]
    public async Task NotificationSavedInRedis()
    {
        await Task.Delay(TimeSpan.FromMinutes(2));
        
        var notifications = await CountNotificationsByEmailAsync("admin@gmail.com");

        // Assert
        notifications.Should().BeGreaterThan(0);
    }
    
    public async Task<long> CountNotificationsByEmailAsync(string email)
    {
        var pattern = $"notifications:{email}:*";
        long count = 0;
        var _redisConnection = ConnectionMultiplexer.Connect(fixture.RedisFixture.ConnectionString);
        var _redisDatabase = _redisConnection.GetDatabase();

        var cursor = 0;
        do
        {
            var scanResult = await _redisDatabase.ExecuteAsync("SCAN", cursor, "MATCH", pattern, "COUNT", 100);
            var response = (RedisResult[])scanResult;

            cursor = int.Parse((string)response[0]);

            var keys = (RedisResult[])response[1];
            foreach (var key in keys)
            {
                var value = await _redisDatabase.StringGetAsync((string)key);
                testOutputHelper.WriteLine($"Ключ: {key}, Значение: {value}");
            }

            count += keys.Length;
        } while (cursor != 0);

        testOutputHelper.WriteLine($"Общее количество найденных ключей: {count}");
        return count;
    }

}