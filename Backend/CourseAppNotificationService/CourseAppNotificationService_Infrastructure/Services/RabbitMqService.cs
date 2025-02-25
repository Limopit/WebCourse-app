using System.Text;
using System.Text.Json;
using CourseAppNotificationService_Domain;
using CourseAppNotificationService_Domain.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

public class RabbitMqService : IRabbitMqService, IAsyncDisposable
{
    private readonly IConnection _connection;
    private readonly IChannel _channel;
    private readonly string _queueName;

    public RabbitMqService(IConfiguration configuration)
    {
        var hostName = configuration["RabbitMQ:HostName"];
        var port = int.Parse(configuration["RabbitMQ:Port"]);
        var userName = configuration["RabbitMQ:UserName"];
        var password = configuration["RabbitMQ:Password"];
        _queueName = configuration["RabbitMQ:QueueName"];

        Console.WriteLine($"Queue name: {_queueName}");

        var factory = new ConnectionFactory
        {
            HostName = hostName,
            Port = port,
            UserName = userName,
            Password = password
        };

        _connection = factory.CreateConnectionAsync().Result;
        _channel = _connection.CreateChannelAsync().Result;

        _channel.QueueDeclareAsync(_queueName, durable: true, exclusive: false, autoDelete: false);
        Console.WriteLine($"Queue declared: {_queueName}");
    }

    public async Task PublishAsync(Notification notification)
    {
        var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(notification));
        await _channel.BasicPublishAsync(exchange: string.Empty, routingKey: _queueName, body: body);

        Console.WriteLine($"Notification published to queue {_queueName}: {notification.Message}");
    }

    public async Task<IEnumerable<Notification>> GetPendingNotificationsAsync(string email)
    {
        var notifications = new List<Notification>();
        var otherNotifications = new List<Notification>();

        while (true)
        {
            var result = await _channel.BasicGetAsync(_queueName, autoAck: false);
            if (result == null)
            {
                Console.WriteLine("No more messages in the queue.");
                break;
            }

            var body = result.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine($"Received message from RabbitMQ: {message}");

            try
            {
                var notification = JsonSerializer.Deserialize<Notification>(message);
                if (notification != null)
                {
                    if (notification.Email == email)
                    {
                        notifications.Add(notification);
                    }
                    else
                    {
                        otherNotifications.Add(notification);
                    }
                }
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Failed to deserialize message: {ex.Message}");
            }

            await _channel.BasicAckAsync(result.DeliveryTag, multiple: false);
        }

        foreach (var otherNotification in otherNotifications)
        {
            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(otherNotification));
            await _channel.BasicPublishAsync(exchange: string.Empty, routingKey: _queueName, body: body);
        }

        return notifications;
    }


    public async ValueTask DisposeAsync()
    {
        await _channel.CloseAsync();
        await _connection.CloseAsync();
    }
}