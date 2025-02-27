using System.Text;
using System.Text.Json;
using CourseAppUserService_Application.Interfaces.Services;
using CourseAppUserService_Domain.Entities;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

namespace CourseAppUserService_Persistance.Services;

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

        _channel.QueueDeclareAsync(_queueName, durable: true, exclusive: false, autoDelete: false).Wait();

        Console.WriteLine($"Queue declared: {_queueName}");
    }

    public async Task PublishAsync(Notification notification)
    {
        var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(notification));
        await _channel.BasicPublishAsync(exchange: string.Empty, routingKey: _queueName, body: body);

        Console.WriteLine($"Notification published to queue {_queueName}: {notification.Message}");
    }

    public async ValueTask DisposeAsync()
    {
        await _channel.CloseAsync();
        await _connection.CloseAsync();
    }
}